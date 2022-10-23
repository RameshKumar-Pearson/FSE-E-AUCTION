using System;
using System.Collections.Generic;
using System.Text;

namespace E_auction.Business.Models
{
    /// <summary>
    /// Model used to get the list of products
    /// </summary>
    public class ProductList
    {
        /// <summary>
        /// Gets (or) sets the product id from mongodb
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets (or) sets the product name from mongodb
        /// </summary>
        public string Name { get; set; }
    }
}
