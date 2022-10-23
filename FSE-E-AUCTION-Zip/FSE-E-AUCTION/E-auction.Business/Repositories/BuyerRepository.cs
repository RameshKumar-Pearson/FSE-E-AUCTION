using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using E_auction.Business.Models;
using E_auction.Business.ResponseModels;

namespace E_auction.Business.Repositories
{
    /// <summary>
    /// Class used to manages the buyer activities
    /// </summary>
    public class BuyerRepository : IBuyerRepository
    {
        private readonly IMongoCollection<MongoBuyerResponse> _buyerCollection;

        #region Public Methods

        /// <summary>
        /// Constructor for <see cref="BuyerRepository"/>
        /// </summary>
        /// <param name="settings">Specifies to gets the <see cref="DbConfiguration"/></param>
        public BuyerRepository(IOptions<DbConfiguration> settings)
        {
            var dbConfiguration = settings.Value;
            var client = new MongoClient(dbConfiguration.ConnectionString);
            var database = client.GetDatabase(dbConfiguration.DatabaseName);
            _buyerCollection = database.GetCollection<MongoBuyerResponse>(dbConfiguration.CollectionName);
        }

        /// <inheritdoc/>
        public async Task UpdateBidAsync(string productId, string email, int newBid)
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
        /// <returns><see cref="MongoBuyerResponse"/></returns>
        private async Task<List<MongoBuyerResponse>> GetBuyerAsync()
        {
            return await _buyerCollection.Find(c => true).ToListAsync();
        }

        #endregion
    }
}
