using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using E_auction.Business.Models;
using E_auction.Business.Contract.CommandHandlers;
using E_auction.Business.Handlers.CommandsHandlers;
using E_auction.Business.Repositories;
using E_auction.Business.Directors;
using E_auction.Business.Validation;
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
    }
}