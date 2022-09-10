
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using SellerApi.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace SellerApi.Repositories
{
    /// <summary>
    /// Class used to manages the seller activities
    /// </summary>
    public class SellerRepository : ISellerRepository
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<SellerDetails> _sellerCollection;
        private readonly DbConfiguration _settings;

        /// <summary>
        /// Constructor for <see cref="SellerRepository"/>
        /// </summary>
        /// <param name="settings">Specifies to gets the db configuration</param>
        public SellerRepository(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _productCollection = database.GetCollection<Product>("Product_Details");
            _sellerCollection = database.GetCollection<SellerDetails>(_settings.CollectionName);
        }

        ///<inheritdoc/>
        public async Task AddProduct(ProductDetails productDetails)
        {
            await _sellerCollection.InsertOneAsync(productDetails.sellerDetails).ConfigureAwait(false);
            productDetails.Details.SellerId = productDetails.sellerDetails.Id;
            await _productCollection.InsertOneAsync(productDetails.Details);
        }

        ///<inheritdoc/>
        public async Task<Product> ShowBids(string ProductId)
        {
            return await _productCollection.Find(c => c.Id == ProductId).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        ///<inheritdoc/>
        public async Task<DeleteResult> DeleteProductAsync(string ProductId)
        {
            return await _productCollection.DeleteOneAsync(c => c.Id == ProductId);
        }
    }
}
