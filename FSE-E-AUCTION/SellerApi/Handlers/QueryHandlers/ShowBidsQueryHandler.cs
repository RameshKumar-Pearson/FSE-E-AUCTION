using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SellerApi.Contract.QueryHandlers;
using SellerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerApi.Handlers.QueryHandlers
{
    /// <summary>
    /// CQRS Query Handler to show the bids o the product
    /// </summary>
    public class ShowBidsQueryHandler : IQueryHandler
    {
        private readonly IMongoCollection<MongoProduct> _productCollection;
        private readonly DbConfiguration _settings;
        private readonly IMongoCollection<SaveBuyerRequestModel> _buyerCollection;

        #region public methods

        /// <summary>
        /// Constructor for <see cref="ShowBidsQueryHandler"/>
        /// </summary>
        /// <param name="settings">Specifies to gets the <see cref="DbConfiguration"/></param>
        public ShowBidsQueryHandler(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _productCollection = database.GetCollection<MongoProduct>("product_details");
            _buyerCollection = database.GetCollection<SaveBuyerRequestModel>("buyer_details");
        }

        ///<inheritdoc/>
        public async Task<ProductBids> ShowBids(string productId)
        {
            var buyerDetails = await _buyerCollection.Find(x => x.ProductId == productId).ToListAsync();
            var productDetails = await _productCollection.Find(x => x.Id == productId).FirstOrDefaultAsync();
            string bids = "";

            foreach (var item in buyerDetails)
            {
                if (!string.IsNullOrWhiteSpace(bids))
                {
                    bids = bids + "," + item.BidAmount;
                }
                else
                {
                    bids = item.BidAmount;
                }
            }
            return new ProductBids
            {
                Name = productDetails.Name,
                ShortDescription = productDetails.ShortDescription,
                DetailedDescription = productDetails.DetailedDescription,
                Bids = bids
            };
        }

        #endregion
    }
}
