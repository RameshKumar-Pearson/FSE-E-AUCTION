using BuyerApi.Models;
using BuyerApi.RequestModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BuyerApi.Contracts.CommandHandlers;

namespace BuyerApi.Handlers.CommandsHandlers
{
    /// <summary>
    /// CQRS Buyer Command Handler used to manages the buyer activities
    /// </summary>
    public class SaveBuyerCommandHandler : ISaveBuyerCommandHandler
    {
        private readonly IMongoCollection<SaveBuyerRequestModel> _buyerCollection;
        private readonly DbConfiguration _settings;

        /// <summary>
        /// Constructor for <see cref="SaveBuyerCommandHandler"/>
        /// </summary>
        /// <param name="settings">Specifies to gets the <see cref="DbConfiguration"/> details</param>
        public SaveBuyerCommandHandler(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _buyerCollection = database.GetCollection<SaveBuyerRequestModel>(_settings.CollectionName);
        }

        /// <summary>
        /// Method used to addd the bid to the existing product
        /// </summary>
        /// <param name="buyerDetails">Specifies to gets the <see cref="SaveBuyerRequestModel"/></param>
        /// <returns>Awaitable task with no data</returns>
        public async Task AddBid(SaveBuyerRequestModel buyerDetails)
        {
            await _buyerCollection.InsertOneAsync(buyerDetails);
        }
    }
}
