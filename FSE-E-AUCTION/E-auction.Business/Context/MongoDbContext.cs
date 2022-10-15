using E_auction.Business.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace E_auction.Business.Context
{
    /// <summary>
    /// Model used to get the <see cref="MongoDbContext"/>
    /// </summary>
    public class MongoDbContext : IMongoDbContext
    {
        private IMongoDatabase Db { get; set; }
        private MongoClient MongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }

        ///<inheritdoc cref="MongoDbContext"/>
        public MongoDbContext(IOptions<DbConfiguration> settings)
        {
            MongoClient = new MongoClient(settings.Value.ConnectionString);
            Db = MongoClient.GetDatabase(settings.Value.DatabaseName);
        }

        ///<inheritdoc cref="IMongoCollection{TDocument}"/>
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Db.GetCollection<T>(name);
        }
    }
}