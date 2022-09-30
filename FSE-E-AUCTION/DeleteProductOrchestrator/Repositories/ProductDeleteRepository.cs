using DeleteProductOrchestrator.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeleteProductOrchestrator.Repositories
{
    /// <summary>
    /// Repository used to delete the product
    /// </summary>
    public class ProductDeleteRepository : IProductDeleteRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        /// <summary>
        /// constructor for <see cref="ProductDeleteRepository"/>
        /// </summary>
        public ProductDeleteRepository()
        {
            var mongoClientSettings = MongoClientSettings.FromConnectionString("mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb");
            var client = new MongoClient("mongodb://fseeauction:6XXU0qrKrijEPmQARM2EHcQ6y926M6R3bONSYHlqaVl0VEzit65prvz275XQ4YxcI61zCYTxWWFlXSR6Yh0GEg==@fseeauction.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@fseeauction@");
            var database = client.GetDatabase("e-auction");
            _productCollection = database.GetCollection<Product>("product_details");
        }

        ///<inheritdoc/>
        public async Task<bool> DeleteProductAsync(string ProductId)
        {
            await _productCollection.DeleteOneAsync(c => c.Id == ProductId);
            return true;
        }
    }
}
