using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace E_auction.Business.Models
{
    /// <summary>
    /// Model used to gets the created product details from mongo
    /// </summary>
    public class ProductResponse
    {
        /// <summary>
        /// Gets (or) Sets the product id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string  ProductId { get; set; }

        /// <summary>
        /// Gets (or) Sets the seller id
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string SellerId { get; set; }
    }
}
