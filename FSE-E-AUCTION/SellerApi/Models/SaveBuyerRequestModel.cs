using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SellerApi.Models
{
    /// <summary>
    /// Model Used to Collect the buyer details
    /// </summary>
    public class SaveBuyerRequestModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets (or) Sets the firstName of the Buyer
        /// </summary>
        [Required(ErrorMessage = "Buyer's First Name Should Not Be Empty")]
        [MinLength(5, ErrorMessage = "Buyer's First Name Should Have Minimum Length 5")]
        [MaxLength(30, ErrorMessage = "Buyer's First Name Should Have Maximum Length 30")]
        public string FirstName { get; set; }


        /// <summary>
        /// Gets (or) Sets the lastName of the Buyer 
        /// </summary>
        [Required(ErrorMessage = "Buyer's Last Name Should Not Be Empty")]
        [MinLength(3, ErrorMessage = "Buyer's Last Name Should Have Minimum Length 3")]
        [MaxLength(25, ErrorMessage = "Buyer's Last Name Should Have Maximum Length 25")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets (or) Sets the address of the Buyer 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets (or) Sets the city of the Buyer 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets (or) Sets the state of the Buyer 
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets (or) Sets the pin of the Buyer 
        /// </summary>
        public int Pin { get; set; }

        /// <summary>
        ///  Gets (or) Sets the phone of the Buyer 
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }

        /// <summary>
        ///  Gets (or) Sets the email of the Buyer 
        /// </summary>
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets (or) Sets the product id of the buyer
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets (or) Sets the bid amount of the buyer
        /// </summary>
        public string BidAmount { get; set; }
    }
}
