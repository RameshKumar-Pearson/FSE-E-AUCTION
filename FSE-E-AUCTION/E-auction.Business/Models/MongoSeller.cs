using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace E_auction.Business.Models
{
    /// <summary>
    /// Model used to gets the seller details
    /// </summary>
    public class MongoSeller
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Gets (or) Sets the firstName of the Seller
        /// </summary>
        [Required(ErrorMessage = "Seller's First Name Should Not Be Empty")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Seller's First Name Should Have Minimum Length 5 and Maximum Length 30")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets (or) Sets the lastName of the seller 
        /// </summary>
        [Required(ErrorMessage = "Seller's Last Name Should Not Be Empty")]
        [StringLength(maximumLength: 25, MinimumLength = 3, ErrorMessage = "Seller's Last Name Should Have Minimum Length 5 and Maximum Length 30")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets (or) Sets the address of the seller 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets (or) Sets the city of the seller 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets (or) Sets the state of the seller 
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets (or) Sets the pin of the seller 
        /// </summary>
        public int Pin { get; set; }

        /// <summary>
        ///  Gets (or) Sets the phone of the seller 
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }

        /// <summary>
        ///  Gets (or) Sets the email of the seller 
        /// </summary>
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

    }
}
