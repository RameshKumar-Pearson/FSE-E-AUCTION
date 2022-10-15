using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_auction.Business.Models
{
    /// <summary>
    /// Model used to get the product details from mongo db
    /// </summary>
    public class MongoProduct
    {
        /// <summary>
        /// Bson id of the product
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Gets (or) Sets the name of the product
        /// </summary>
        [Required(ErrorMessage = "Product Name Should Not Be Empty")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Product Name Should Have Minimum Length 5 and Maximum Length 30")]
        public string Name { get; set; }

        /// <summary>
        /// Gets (or) Sets the short-description of the product
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets (or) Sets the detailed-description of the product
        /// </summary>
        public string DetailedDescription { get; set; }

        /// <summary>
        /// Gets (or) Sets the category of the product
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets (or) Sets the starting price of the product
        /// </summary>
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Please enter valid price")]
        public int StartingPrice { get; set; }

        /// <summary>
        /// Gets (or) Sets the bid end date
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime BidEndDate { get; set; }

        /// <summary>
        /// Gets (or) Sets the seller Id 
        /// </summary>
        public string SellerId { get; set; }
    }
}
