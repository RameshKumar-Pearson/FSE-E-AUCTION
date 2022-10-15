namespace E_auction.Business.Models
{
    /// <summary>
    /// Model used to get the email address and display name of the recipient
    /// </summary>
    public class EmailAddress
    {
        /// <summary>
        /// Gets (or) sets the email address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets (or) sets the display name
        /// </summary>
        public string DisplayName { get; set; }
    }
}