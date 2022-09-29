using DeleteProductOrchestrator.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeleteProductOrchestrator.Repositories
{
    public class ProductDeleteRepository : IProductDeleteRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductDeleteRepository()
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb");
            client.Settings.MaxConnectionIdleTime = new TimeSpan(0, 3, 0);
            var database = client.GetDatabase("e-auction");
            _productCollection = database.GetCollection<Product>("Product_Details");
        }

        ///<inheritdoc/>
        public async Task<bool> DeleteProductAsync(string ProductId)
        {
            await _productCollection.DeleteOneAsync(c => c.Id == ProductId);
            return true;
        }
    }
}
