
using E_auction.Business.Contract.CommandHandlers;
using E_auction.Business.Handlers.CommandsHandlers;
using Eauctionbideventhandler;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Eauctionbideventhandler
{
    public class Startup : FunctionsStartup
    {
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<ISaveBuyerCommandHandler, SaveBuyerCommandHandler>();
        }
    }
}
