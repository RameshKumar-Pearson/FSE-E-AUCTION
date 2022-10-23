using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using MassTransit;
using System.Net;
using System.Reflection;
using E_auction.Business.Models;
using E_auction.Business.Contract.CommandHandlers;
using E_auction.Business.Handlers.CommandsHandlers;
using E_auction.Business.Repositories;
using E_auction.Business.Directors;
using E_auction.Business.Validation;
using E_auction.Business.KafkaConsumerService;
using E_auction.Business.MessagePublishers;
using Microsoft.Azure.ServiceBus;

namespace BuyerApi
{
    /// <summary>
    /// StartUp Class for Buyer Api
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor for <see cref="Startup"/>
        /// </summary>
        /// <param name="configuration">Specifies to gets the <see cref="IConfiguration"/></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        #region Public Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISaveBuyerCommandHandler, SaveBuyerCommandHandler>();
            services.AddTransient<IBuyerRepository, BuyerRepository>();
            services.AddTransient<IBuyerDirector, BuyerDirector>();
            services.AddTransient<IBuyerValidation, BuyerValidation>();
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddSingleton<ISubscriptionClient>(serviceProvider => new SubscriptionClient(
                Configuration.GetValue<string>("servicebus:connectionstring"),
                Configuration.GetValue<string>("serviceBus:topicname"),
                Configuration.GetValue<string>("serviceBus:subscription")));
            services.Configure<DbConfiguration>(Configuration.GetSection("MongoDbConnection"));
            services.AddControllers();
            services.AddApplicationInsightsTelemetry();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BuyerApi", Version = "v1" });
            });

            services.AddMassTransit(x =>
            {
                x.UsingInMemory((context, config) => config.ConfigureEndpoints(context));

                x.AddRider(rider =>
                {
                    rider.AddConsumer<BuyerEventConsumer>();
                    rider.AddProducer<KafkaBuyerEventCreate>(nameof(KafkaBuyerEventCreate));

                    rider.UsingKafka((context, k) =>
                    {
                        k.Host("localhost:9092");

                        k.TopicEndpoint<KafkaBuyerEventCreate>(nameof(KafkaBuyerEventCreate),
                            GetUniqueName(nameof(KafkaBuyerEventCreate)), e =>
                            {
                                // e.AutoOffsetReset = AutoOffsetReset.Latest;
                                //e.ConcurrencyLimit = 3;
                                e.CheckpointInterval = TimeSpan.FromSeconds(10);
                                e.ConfigureConsumer<BuyerEventConsumer>(context);
                            });
                    });
                });
            });
            services.AddSingleton<ITopicClient>(serviceProvider =>
                new TopicClient(Configuration.GetValue<string>("servicebus:connectionstring"),
                    Configuration.GetValue<string>("serviceBus:topic")));
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddSingleton<ISubscriptionClient>(serviceProvider => new SubscriptionClient(
                Configuration.GetValue<string>("servicebus:connectionstring"),
                Configuration.GetValue<string>("serviceBus:topic"),
                Configuration.GetValue<string>("serviceBus:subscription")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BuyerApi v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseDefaultFiles();

            app.UseStaticFiles();
        }

        #endregion

        #region Private Methods

        private static string GetUniqueName(string eventName)
        {
            var hostName = Dns.GetHostName();
            var callingAssembly = Assembly.GetCallingAssembly().GetName().Name;
            return $"{hostName}.{callingAssembly}.{eventName}";
        }

        #endregion
    }
}