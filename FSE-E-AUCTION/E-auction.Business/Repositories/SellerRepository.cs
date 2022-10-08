
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;
using Microsoft.Extensions.Options;
using System;
using E_auction.Business.Models;
using E_auction.Business.RequestModels;
using MongoDB.Bson;

namespace E_auction.Business.Repositories
{
    /// <summary>
    /// Class used to manages the seller activities
    /// </summary>
    public class SellerRepository : ISellerRepository
    {
        private readonly IMongoCollection<BsonDocument> _productCollection;
        private readonly IMongoCollection<BsonDocument> _sellerCollection;
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
            _productCollection = database.GetCollection<BsonDocument>("product_details");
            _sellerCollection = database.GetCollection<BsonDocument>(_settings.CollectionName);
        }

        ///<inheritdoc/>
        public async Task<ProductResponse> AddProduct(ProductDetails productDetails)
        {
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
                StartingPrice= Convert.ToInt32(productDetails.StartingPrice),
                Id = MongoDB.Bson.ObjectId.GenerateNewId(),
                SellerId = sellerDetails.Id.ToString()
            };

            await _sellerCollection.InsertOneAsync(sellerDetails.ToBsonDocument()).ConfigureAwait(false);

            await _productCollection.InsertOneAsync(product.ToBsonDocument());

            return new ProductResponse { ProductId = product.Id, SellerId = product.SellerId };
        }

        ///<inheritdoc/>
        public async Task<DeleteResult> DeleteProductAsync(string ProductId)
        {
            ObjectId bsonObjectId;
            ObjectId.TryParse(ProductId, out bsonObjectId);
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", bsonObjectId);
            return await _productCollection.DeleteOneAsync(deleteFilter); ;
        }
    }
}
