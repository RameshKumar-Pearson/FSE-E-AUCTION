using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace E_auction.Business.Models
{
    /// <summary>
    /// class used to show the bids details of the product
    /// </summary>
    public class ProductBids
    {
        /// <summary>
        /// Gets(or) Sets the product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets(or) Sets the product short description
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Gets(or) Sets the product detailed description
        /// </summary>
        public string DetailedDescription { get; set; }

        /// <summary>
        /// Gets(or) Sets the product bids
        /// </summary>
        public List<BidDetails> Bids { get; set; }

        /// <summary>
        /// Gets (or) sets the category of the product
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets (or) sets the starting price of the product
        /// </summary>
        public int StartingPrice { get; set; }

        /// <summary>
        /// Gets (or) sets the Bid end date  of the product
        /// </summary>
        public DateTime BidEndData { get; set; }
    }
}
