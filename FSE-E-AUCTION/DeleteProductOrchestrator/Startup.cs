
using DeleteProductOrchestrator;
using E_auction.Business.Directors;
using E_auction.Business.Repositories;
using EauctionBidEventHandler;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Startup))]
namespace DeleteProductOrchestrator
{
    public class Startup : FunctionsStartup
    {
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration configuration = ConfigurationStartup.AddConfigurationProviders(builder.Services);
            builder.Services.Configure<E_auction.Business.Models.DbConfiguration>(configuration.GetSection("MongoDbConnection"));
            builder.Services.AddTransient<ISellerDirector, SellerDirector>();
            builder.Services.AddTransient<ISellerRepository, SellerRepository>();
        }
    }
}
