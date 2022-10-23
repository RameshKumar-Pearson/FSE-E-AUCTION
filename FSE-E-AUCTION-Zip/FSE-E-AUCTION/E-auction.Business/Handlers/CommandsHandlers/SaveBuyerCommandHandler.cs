using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;
using E_auction.Business.Contract.CommandHandlers;
using E_auction.Business.Models;
using E_auction.Business.RequestModels;

namespace E_auction.Business.Handlers.CommandsHandlers
{
    /// <summary>
    /// CQRS Buyer Command Handler used to manages the buyer activities
    /// </summary>
    public class SaveBuyerCommandHandler : ISaveBuyerCommandHandler
    {
        private readonly IMongoCollection<SaveBuyerRequestModel> _buyerCollection;

        /// <summary>
        /// Constructor for <see cref="SaveBuyerCommandHandler"/>
        /// </summary>
        /// <param name="settings">Specifies to gets the <see cref="DbConfiguration"/> details</param>
        public SaveBuyerCommandHandler(IOptions<DbConfiguration> settings)
        {
            var dbConfiguration = settings.Value;
            var client = new MongoClient(dbConfiguration.ConnectionString);
            var database = client.GetDatabase(dbConfiguration.DatabaseName);
            _buyerCollection = database.GetCollection<SaveBuyerRequestModel>(dbConfiguration.CollectionName);
        }

        /// <summary>
        /// Method used to add the bid to the existing product
        /// </summary>
        /// <param name="buyerDetails">Specifies to gets the <see cref="SaveBuyerRequestModel"/></param>
        /// <returns>Awaitable task with no data</returns>
        public async Task AddBidAsync(SaveBuyerRequestModel buyerDetails)
        {
            await _buyerCollection.InsertOneAsync(buyerDetails);
        }
    }
}
