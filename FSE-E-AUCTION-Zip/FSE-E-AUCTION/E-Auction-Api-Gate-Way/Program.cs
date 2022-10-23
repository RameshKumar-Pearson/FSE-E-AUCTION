using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace E_Auction_Api_Gate_Way
{
    /// <summary>
    /// Program class for e-auction api gateway
    /// </summary>
    public class Program
    {
        /// <summary>
        ///  Entry method for api-gate-way
        /// </summary>
        /// <param name="args">Specifies to gets th program arguments </param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Method used to configure startUp class 
        /// </summary>
        /// <param name="args">Specifies to gets th program arguments</param>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("Ocelot.json", false, true);
                });
        }
    }
}