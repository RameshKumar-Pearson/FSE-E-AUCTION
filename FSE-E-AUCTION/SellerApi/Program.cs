using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SellerApi
{
    /// <summary>
    /// Program class for <see cref="Program"/>
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); }).ConfigureLogging(
                    logging =>
                    {
                        // clear default logging providers
                        logging.ClearProviders();

                        // add built-in providers manually, as needed 
                        logging.AddConsole();
                        logging.AddDebug();
                        logging.AddEventLog();
                        logging.AddEventSourceLogger();
                    });
        }
    }
}