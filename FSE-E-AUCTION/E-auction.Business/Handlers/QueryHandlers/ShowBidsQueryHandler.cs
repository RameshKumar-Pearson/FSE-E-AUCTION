using System.Linq;
using E_auction.Business.Contract.QueryHandlers;
using E_auction.Business.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;
using E_auction.Business.ResponseModels;

namespace E_auction.Business.Handlers.QueryHandlers
{
    /// <summary>
    /// CQRS Query Handler to show the bids o the product
    /// </summary>
    public class ShowBidsQueryHandler : IQueryHandler
    {
        private readonly IMongoCollection<MongoProduct> _productCollection;
        private readonly IMongoCollection<MongoBuyerResponse> _buyerCollection;

        #region public methods

        /// <summary>
        /// Constructor for <see cref="ShowBidsQueryHandler"/>
        /// </summary>
        /// <param name="settings">Specifies to gets the <see cref="DbConfiguration"/></param>
        public ShowBidsQueryHandler(IOptions<DbConfiguration> settings)
        {
            var dbConfiguration = settings.Value;
            var client = new MongoClient(dbConfiguration.ConnectionString);
            var database = client.GetDatabase(dbConfiguration.DatabaseName);
            _productCollection = database.GetCollection<MongoProduct>("product_details");
            _buyerCollection = database.GetCollection<MongoBuyerResponse>("buyer_details");
        }

        ///<inheritdoc/>
        public async Task<ProductBids> ShowBids(string productId)
        {
            var buyerDetails = await _buyerCollection.Find(x => x.ProductId == productId).ToListAsync();
            var productDetails = await _productCollection.Find(x => x.Id == productId).FirstOrDefaultAsync();
            var bids = buyerDetails.Select(item => new BidDetails { BidAmount = item.BidAmount, MobileNumber = item.Phone, Name = item.FirstName + "" + item.LastName, Email = item.Email }).ToList();

            return new ProductBids
            {
                Name = productDetails.Name,
                ShortDescription = productDetails.ShortDescription,
                DetailedDescription = productDetails.DetailedDescription,
                Category = productDetails.Category,
                BidEndData = productDetails.BidEndDate.ToShortDateString(),
                StartingPrice = productDetails.StartingPrice,
                Bids = bids
            };

        }

        #endregion
    }
}
