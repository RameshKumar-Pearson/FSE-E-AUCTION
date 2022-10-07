using E_auction.Business.Contract.CommandHandlers;
using E_auction.Business.Handlers.CommandsHandlers;
using EauctionBidEventHandler;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Startup))]
namespace EauctionBidEventHandler
{
   public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration configuration = ConfigurationStartup.AddConfigurationProviders(builder.Services);
            builder.Services.AddTransient<ISaveBuyerCommandHandler, SaveBuyerCommandHandler>();
            builder.Services.Configure<E_auction.Business.Models.DbConfiguration>(configuration.GetSection("MongoDbConnection"));
        }
    }
}
