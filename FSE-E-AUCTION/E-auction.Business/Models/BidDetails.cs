using System;
using System.Collections.Generic;
using System.Text;

namespace E_auction.Business.Models
{
    public class BidDetails
    {
        /// <summary>
        /// Gets (or) sets the buyer name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets (or) sets the bid amount
        /// </summary>
        public string BidAmount { get; set; }

        /// <summary>
        /// Gets (or) sets the buyer email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets (or) sets buyer mobile number
        /// </summary>
        public string MobileNumber { get; set; }

    }
}
