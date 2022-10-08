using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_auction.Business.Models
{
   public class MongoProductStaging
    {
        [BsonId]
        public ObjectId Id { get; set; }
     
        /// <summary>
        /// Gets (or) Sets the name of the product
        /// </summary>
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
        public int StartingPrice { get; set; }

        /// <summary>
        /// Gets (or) Sets the bid end date
        /// </summary>
        public DateTime BidEndDate { get; set; }

        /// <summary>
        /// Gets (or) Sets the seller Id 
        /// </summary>
        public string SellerId { get; set; }
    }
}
