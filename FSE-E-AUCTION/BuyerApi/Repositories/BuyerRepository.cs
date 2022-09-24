using BuyerApi.Models;
using BuyerApi.RequestModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyerApi.Repositories
{
    /// <summary>
    /// Class used to manages the buyer activities
    /// </summary>
    public class BuyerRepository : IBuyerRepository
    {
        private readonly IMongoCollection<SaveBuyerRequestModel> _buyerCollection;
        private readonly DbConfiguration _settings;

        #region Public Methods

        /// <summary>
        /// Constructor for <see cref="BuyerRepository"/>
        /// </summary>
        /// <param name="settings">Specifies to gets the <see cref="DbConfiguration"/></param>
        public BuyerRepository(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _buyerCollection = database.GetCollection<SaveBuyerRequestModel>(_settings.CollectionName);
        }

        /// <inheritdoc/>
        public async Task UpdateBid(string productId, string email, string newBid)
        {
            var buyersList = await GetBuyerAsync();
            var buyerDetails = buyersList.Where(x => x.Email == email && x.ProductId == productId).Select(o => o).FirstOrDefault();
            if (buyerDetails != null)
            {
                buyerDetails.BidAmount = newBid;
            }

            await _buyerCollection.ReplaceOneAsync(x => x.Id == buyerDetails.Id, buyerDetails);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method used to gets the buyer details
        /// </summary>
        /// <returns><see cref="SaveBuyerRequestModel"/></returns>
        private async Task<List<SaveBuyerRequestModel>> GetBuyerAsync()
        {
            return await _buyerCollection.Find(c => true).ToListAsync();
        }

        #endregion
    }
}
