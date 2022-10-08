
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
        private readonly IMongoCollection<MongoProduct> _productCollection;
        private IMongoDatabase mongoDatabase;

        /// <summary>
        /// constructor for <see cref="ProductDeleteRepository"/>
        /// </summary>
        public ProductDeleteRepository()
        {
            var client = new MongoClient("mongodb://fseeauction:6XXU0qrKrijEPmQARM2EHcQ6y926M6R3bONSYHlqaVl0VEzit65prvz275XQ4YxcI61zCYTxWWFlXSR6Yh0GEg==@fseeauction.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@fseeauction@");
              mongoDatabase = client.GetDatabase("e-auction");
            _productCollection = mongoDatabase.GetCollection<MongoProduct>("product_details");
        }

        ///<inheritdoc/>
        public async Task<bool> DeleteProductAsync(string ProductId)
        {
            bool isDeleted = false;

            ObjectId test;

            ObjectId.TryParse(ProductId, out test);

            var filter = Builders<MongoProduct>.Filter.Eq(product => product.Id, test);
            var productDeleteResult = await _productCollection.DeleteOneAsync(filter);

            if (productDeleteResult.DeletedCount == 1)
            {
                isDeleted = true;
            }

            return isDeleted;
        }
    }
}
