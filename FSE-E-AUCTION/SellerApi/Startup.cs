using E_auction.Business.Contract.QueryHandlers;
using E_auction.Business.Directors;
using E_auction.Business.Handlers.QueryHandlers;
using E_auction.Business.MessagePublishers;
using E_auction.Business.Repositories;
using E_auction.Business.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

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
            services.Configure<E_auction.Business.Models.DbConfiguration>(Configuration.GetSection("MongoDbConnection"));
            services.AddControllers();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SellerApi", Version = "v1" });
            });
            services.AddSingleton<ITopicClient>(serviceProvider => new TopicClient(connectionString: Configuration.GetValue<string>("servicebus:connectionstring"), entityPath: Configuration.GetValue<string>("serviceBus:topicname")));
            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddSingleton<ISubscriptionClient>(serviceProvider => new SubscriptionClient(
            connectionString: Configuration.GetValue<string>("servicebus:connectionstring"),
            topicPath: Configuration.GetValue<string>("serviceBus:topicname"), subscriptionName: Configuration.GetValue<string>("serviceBus:subscription")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SellerApi v1"));
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }

        private static string GetUniqueName(string eventName)
        {
            string hostName = Dns.GetHostName();
            string callingAssembly = Assembly.GetCallingAssembly().GetName().Name;
            return $"{hostName}.{callingAssembly}.{eventName}";
        }
    }
}
