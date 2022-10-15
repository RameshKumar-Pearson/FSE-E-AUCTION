using E_auction.Business.Context;
using E_auction.Business.Directors;
using E_auction.Business.Models;
using E_auction.Business.Repositories;
using E_auction.Business.Services.EmailService;
using EauctionDeleteEventHandler;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace EauctionDeleteEventHandler
{
    public class Startup : FunctionsStartup
    {
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration configuration = ConfigurationStartup.AddConfigurationProviders(builder.Services);
            builder.Services.Configure<DbConfiguration>(
                configuration.GetSection("MongoDbConnection"));
            builder.Services.Configure<EmailConfiguration>(
                configuration.GetSection("EmailConfiguration"));
            builder.Services.AddTransient<ISellerDirector, SellerDirector>();
            builder.Services.AddTransient<ISellerRepository, SellerRepository>();
            builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}
