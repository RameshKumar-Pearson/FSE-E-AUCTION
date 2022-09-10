
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyerApi.Repositories;
using BuyerApi.Contracts.CommandHandlers;
using BuyerApi.Handlers.CommandsHandlers;
using BuyerApi.Models;
using BuyerApi.Directors;
using Confluent.Kafka;

namespace BuyerApi
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
            //Kafka Producer Config
            var producerConfig = new ProducerConfig();
            producerConfig.Acks = Acks.All;
            producerConfig.MessageTimeoutMs= 3000;
            producerConfig.MessageSendMaxRetries = 3;

            Configuration.Bind("Producer", producerConfig);

            //Kafka Consumer Config...
            var consumerConfig = new ConsumerConfig();
            consumerConfig.Acks = Acks.All;
           
            Configuration.Bind("Consumer", consumerConfig);

            services.AddSingleton<ProducerConfig>(producerConfig);
            services.AddSingleton<ConsumerConfig>(consumerConfig);
            services.AddTransient<ISaveBuyerCommandHandler, SaveBuyerCommandHandler>();
            services.AddTransient<IBuyerRepository, BuyerRepository>();
            services.AddTransient<IBuyerDirector, BuyerDirector>();
            services.Configure<DbConfiguration>(Configuration.GetSection("MongoDbConnection"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BuyerApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BuyerApi v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
