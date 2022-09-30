
using System.Threading.Tasks;

namespace E_auction.Business.Repositories
{
    /// <summary>
    /// Interface class used to manage the buyer activities
    /// </summary>
    public interface IBuyerRepository
    {
        /// <summary>
        /// Method used to updates the new bid amount for the existing product
        /// </summary>
        /// <param name="productId">Specifies to gets the productId</param>
        /// <param name="email">Specifies to gets the email address of the user</param>
        /// <param name="newBid">Specifies to gets the new bid amount</param>
        /// <returns>Awaitable task with no data</returns>
        Task UpdateBid(string productId, string email, string newBid);
    }
}
