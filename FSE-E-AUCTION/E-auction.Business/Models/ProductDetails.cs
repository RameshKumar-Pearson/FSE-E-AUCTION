using System;
using System.ComponentModel.DataAnnotations;

namespace E_auction.Business.Models
{
    /// <summary>
    /// Model used to gets the product details for creating product in mongodb
    /// </summary>
    public class ProductDetails
    {
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
        public int StartingPrice { get; set; }

        /// <summary>
        /// Gets (or) Sets the bid end date
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime BidEndDate { get; set; }

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
        public string Phone { get; set; }

        /// <summary>
        ///  Gets (or) Sets the email of the seller 
        /// </summary>
        public string Email { get; set; }

    }
}
