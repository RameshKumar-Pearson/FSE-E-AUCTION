
using DeleteProductOrchestrator;
using E_auction.Business.Contract.CommandHandlers;
using E_auction.Business.Directors;
using E_auction.Business.Handlers.CommandsHandlers;
using E_auction.Business.Repositories;
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
            builder.Services.AddTransient<ISaveBuyerCommandHandler, SaveBuyerCommandHandler>();
        }
    }
}
