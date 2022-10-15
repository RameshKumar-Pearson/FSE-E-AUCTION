namespace E_auction.Business.Models
{
    /// <summary>
    /// Model used to get the DbConfiguration
    /// </summary>
    public class DbConfiguration
    {
        /// <summary>
        /// Specifies to gets the collection name
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        /// Specifies to gets the connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Specifies to gets the database name
        /// </summary>
        public string DatabaseName { get; set; }
    }
}