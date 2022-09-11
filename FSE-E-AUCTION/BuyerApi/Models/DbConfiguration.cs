using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerApi.Models
{
    /// <summary>
    /// Model used to gets/sets the DB configuration values
    /// </summary>
    public class DbConfiguration
    {
        /// <summary>
        /// Gets(or) Sets the collection name of MongoDB
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        ///  Gets(or) Sets the connection string of mongo db
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets (or) Sets the Mongo DB name
        /// </summary>
        public string DatabaseName { get; set; }
    }
}
