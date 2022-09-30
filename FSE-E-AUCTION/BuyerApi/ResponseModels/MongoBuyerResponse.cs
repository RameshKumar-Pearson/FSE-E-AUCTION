using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerApi.ResponseModels
{
    /// <summary>
    /// Model use to gets the buyer details with respect to product
    /// </summary>
    public class MongoBuyerResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets (or) Sets the bid amount of the buyer
        /// </summary>
        public string BidAmount { get; set; }

        /// <summary>
        ///  Gets (or) Sets the email of the Buyer 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets (or) Sets the product id of the buyer
        /// </summary>
        public string ProductId { get; set; }

    }
}
