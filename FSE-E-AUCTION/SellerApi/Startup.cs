using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SellerApi.Contract.QueryHandlers;
using SellerApi.Directors;
using SellerApi.Handlers.QueryHandlers;
using SellerApi.MessagePublishers;
using SellerApi.Models;
using SellerApi.Repositories;
using SellerApi.Validation;
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
            services.Configure<DbConfiguration>(Configuration.GetSection("MongoDbConnection"));
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
            app.UseCors(x=>x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:26045/"));
        }

        private static string GetUniqueName(string eventName)
        {
            string hostName = Dns.GetHostName();
            string callingAssembly = Assembly.GetCallingAssembly().GetName().Name;
            return $"{hostName}.{callingAssembly}.{eventName}";
        }
    }
}
