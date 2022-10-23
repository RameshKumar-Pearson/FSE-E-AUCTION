using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace E_auction.Business.ResponseModels
{
    /// <summary>
    /// Model use to gets the buyer details with respect to product
    /// </summary>
    public class MongoBuyerResponse
    {
        /// <summary>
        /// Bson id of the product
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets (or) Sets the bid amount of the buyer
        /// </summary>
        public int BidAmount { get; set; }

        /// <summary>
        ///  Gets (or) Sets the email of the Buyer 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets (or) Sets the product id of the buyer
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets (or) Sets the firstName of the buyer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets (or) Sets the lastName of the buyer
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets (or) Sets the address of the buyer
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets (or) Sets the city of the buyer
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets (or) Sets the city of the buyer
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets (or) Sets the pin of the buyer
        /// </summary>
        public int Pin { get; set; }

        /// <summary>
        /// Gets (or) Sets the Phone of the buyer
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets (or) Sets the seller Id 
        /// </summary>
        public string SellerId { get; set; }
    }
}