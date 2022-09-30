using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_auction.Business.Models
{
    public class DbConfiguration
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
