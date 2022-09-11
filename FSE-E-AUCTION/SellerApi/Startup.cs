using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SellerApi.Contract.QueryHandlers;
using SellerApi.Directors;
using SellerApi.Handlers.QueryHandlers;
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SellerApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SellerApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static string GetUniqueName(string eventName)
        {
            string hostName = Dns.GetHostName();
            string callingAssembly = Assembly.GetCallingAssembly().GetName().Name;
            return $"{hostName}.{callingAssembly}.{eventName}";
        }
    }
}
