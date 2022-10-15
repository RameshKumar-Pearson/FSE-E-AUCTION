using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;
using Microsoft.Extensions.Options;
using System;
using E_auction.Business.Models;
using E_auction.Business.Context;

namespace E_auction.Business.Repositories
{
    /// <summary>
    /// Class used to manages the seller activities
    /// </summary>
    public class SellerRepository : ISellerRepository
    {
        private IMongoCollection<MongoProduct> _productCollection;
        private IMongoCollection<MongoSeller> _sellerCollection;
        private readonly DbConfiguration _settings;
        private readonly IMongoDbContext _mongoDbContext;

        /// <summary>
        /// Constructor for <see cref="SellerRepository"/>
        /// </summary>
        /// <param name="settings">Specifies to gets the db configuration</param>
        /// <param name="mongoDbContext">Specifies to gets the <see cref="MongoDbContext"/></param>
        public SellerRepository(IOptions<DbConfiguration> settings, IMongoDbContext mongoDbContext)
        {
            _settings = settings.Value;
            _mongoDbContext = mongoDbContext;
        }

        ///<inheritdoc/>
        public async Task<ProductResponse> AddProduct(ProductDetails productDetails)
        {
            _productCollection = _mongoDbContext.GetCollection<MongoProduct>("product_details");
            _sellerCollection = _mongoDbContext.GetCollection<MongoSeller>(_settings.CollectionName);
            var sellerDetails = new MongoSeller
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId(),
                FirstName = productDetails.FirstName,
                LastName = productDetails.LastName,
                Address = productDetails.Address,
                City = productDetails.City,
                State = productDetails.State,
                Email = productDetails.Email,
                Pin = productDetails.Pin,
                Phone = productDetails.Phone
            };

            var product = new MongoProduct
            {
                Name = productDetails.Name,
                ShortDescription = productDetails.ShortDescription,
                DetailedDescription = productDetails.DetailedDescription,
                Category = productDetails.Category,
                BidEndDate = productDetails.BidEndDate,
                StartingPrice = Convert.ToInt32(productDetails.StartingPrice),
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                SellerId = sellerDetails.Id.ToString()
            };

            await _sellerCollection.InsertOneAsync(sellerDetails).ConfigureAwait(false);

            await _productCollection.InsertOneAsync(product);

            return new ProductResponse { ProductId = product.Id, SellerId = product.SellerId };
        }

        ///<inheritdoc/>
        public async Task<DeleteResult> DeleteProductAsync(string productId)
        {
            _productCollection = _mongoDbContext.GetCollection<MongoProduct>("product_details");
            var existingProducts = await _productCollection.Find(c => true).ToListAsync();

            var isExist = existingProducts.FirstOrDefault(x => x.Id.Equals(productId) && x.Id != null);

            if (isExist == null)
            {
                throw new System.Exception("Not found");
            }

            return await _productCollection.DeleteOneAsync(x => x.Id.Equals(productId)).ConfigureAwait(false);
        }
    }
}
