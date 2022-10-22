using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace E_Auction_Api_Gate_Way
{
    /// <summary>
    ///  Start up class for api gateway 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor for <see cref="Startup"/>
        /// </summary>
        /// <param name="configuration">Specifies to gets  <see cref="IConfiguration"/></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets (or) sets configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Specifies to get <see cref="IServiceCollection"/></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddApiExplorer();
            services.AddSwaggerForOcelot(Configuration);
            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Method used to configure applicationBuilder for api gate way
        /// </summary>
        /// <param name="app">Specifies to gets <see cref="IApplicationBuilder"/></param>
        /// <param name="env">Specifies to gets <see cref="IWebHostEnvironment"/></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/gateway");
            app.UseDeveloperExceptionPage();
            app.UseSwagger();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwaggerForOcelotUI(opt =>
                {
                    opt.DownstreamSwaggerEndPointBasePath = "/gateway/swagger/docs";
                    opt.PathToSwaggerGenerator = "/swagger/docs";
                })
                .UseOcelot()
                .Wait();
        }
    }
}