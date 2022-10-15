using E_auction.Business.Context;
using E_auction.Business.Contract.QueryHandlers;
using E_auction.Business.Directors;
using E_auction.Business.Handlers.QueryHandlers;
using E_auction.Business.MessagePublishers;
using E_auction.Business.Models;
using E_auction.Business.Repositories;
using E_auction.Business.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SellerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISellerDirector, SellerDirector>();
            services.AddTransient<ISellerRepository, SellerRepository>();
            services.AddTransient<IQueryHandler, ShowBidsQueryHandler>();
            services.AddTransient<ISellerValidation, SellerValidation>();
            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            services.Configure<DbConfiguration>(Configuration.GetSection("MongoDbConnection"));
            services.AddControllers();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SellerApi", Version = "v1" });
            });
            services.AddSingleton<ITopicClient>(serviceProvider =>
                new TopicClient(Configuration.GetValue<string>("servicebus:connectionstring"),
                    Configuration.GetValue<string>("serviceBus:topicname")));
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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SellerApi v1"));
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }
    }
}