using BuyerApi.Models;
using System;
using System.Threading.Tasks;

namespace BuyerApi.Repositories
{
    /// <summary>
    /// Class used to manages the repository activities
    /// </summary>
    public class BuyerRepository : IBuyerRepository
    {
        ///<inheritdoc/>
        public Task AddBid(BuyerDetails buyerDetails)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBid(string productId, string email, int newBid)
        {
            throw new NotImplementedException();
        }
    }
}
