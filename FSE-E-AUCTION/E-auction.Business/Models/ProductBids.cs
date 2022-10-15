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
        public string Bids { get; set; }
    }
}
