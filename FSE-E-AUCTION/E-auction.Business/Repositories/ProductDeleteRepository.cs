
using E_auction.Business.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_auction.Business.Repositories
{
    /// <summary>
    /// Repository used to delete the product
    /// </summary>
    public class ProductDeleteRepository : IProductDeleteRepository
    {
        private readonly IMongoCollection<BsonDocument> _productCollection;
        private IMongoDatabase mongoDatabase;

        /// <summary>
        /// constructor for <see cref="ProductDeleteRepository"/>
        /// </summary>
        public ProductDeleteRepository()
        {
            var client = new MongoClient("mongodb://fseeauction:6XXU0qrKrijEPmQARM2EHcQ6y926M6R3bONSYHlqaVl0VEzit65prvz275XQ4YxcI61zCYTxWWFlXSR6Yh0GEg==@fseeauction.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@fseeauction@");
              mongoDatabase = client.GetDatabase("e-auction");
            _productCollection = mongoDatabase.GetCollection<BsonDocument>("product_details");
        }

        ///<inheritdoc/>
        public async Task<DeleteResult> DeleteProductAsync(string ProductId)
        {
            ObjectId productId;

            ObjectId.TryParse("6341a3b09c2f57bf4066322b", out productId);

            var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", productId);

            var existingProducts1 = await _productCollection.Find<BsonDocument>(c => true).ToListAsync();

            DeleteResult productDeleteResult = await _productCollection.DeleteOneAsync(deleteFilter);

            return productDeleteResult;
        }
    }
}
