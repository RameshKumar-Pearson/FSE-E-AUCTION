using BuyerApi.Models;
using System;
using System.Threading.Tasks;

namespace BuyerApi.Directors
{
    /// <summary>
    /// Class used to manages the buyer activities
    /// </summary>
    public class BuyerDirector : IBuyerDirector
    {
        ///<inheritdoc/>
        public Task AddBid(BuyerDetails buyerDetails)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBid(string productId, string emailId, int newBid)
        {
            throw new NotImplementedException();
        }
    }
}
