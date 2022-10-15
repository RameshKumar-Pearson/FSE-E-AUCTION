using MongoDB.Driver;

namespace E_auction.Business.Context
{
    /// <summary>
    /// Class used to gets the mongodb context
    /// </summary>
    public interface IMongoDbContext
    {
        /// <summary>
        /// Method used to gets the mongo collection response
        /// </summary>
        /// <typeparam name="T">Specified to gets the type of collection</typeparam>
        /// <param name="name">Specifies to gets the name of the collection</param>
        /// <returns>Mongo collection response</returns>
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
