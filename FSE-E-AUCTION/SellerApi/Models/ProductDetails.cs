using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace SellerApi.Models
{
    public class ProductDetails
    {
        /// <summary>
        /// Gets (or) Sets the seller Id of the product
        /// </summary>
        public SellerDetails sellerDetails { get; set; }

        /// <summary>
        /// Gets (or) Sets the pduct details
        /// </summary>
        public Product Details { get; set; }
    }
}
