using BuyerApi.Models;
using System.Threading.Tasks;

namespace BuyerApi.Repositories
{
    /// <summary>
    /// Interface class used to manage the buyer activities
    /// </summary>
    public interface IBuyerRepository
    {
        /// <summary>
        /// Method used add the buyer details
        /// </summary>
        /// <param name="buyerDetails">Specifies to gets the buyer details</param>
        /// <returns>Awaitable task with no data</returns>
        Task AddBid(BuyerDetails buyerDetails);

        Task UpdateBid(string productId, string email, int newBid);
    }
}
