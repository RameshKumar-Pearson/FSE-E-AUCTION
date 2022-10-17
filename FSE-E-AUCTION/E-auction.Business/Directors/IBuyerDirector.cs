using System.Threading.Tasks;

namespace E_auction.Business.Directors
{
    /// <summary>
    /// Interface class used to manages the buyer activities
    /// </summary>
    public interface IBuyerDirector
    {
        /// <summary>
        /// Method used to Update the bid amount of the existing product
        /// </summary>
        /// <param name="productId">Specifies to gets the productId</param>
        /// <param name="email">Specifies to gets the email of the buyer</param>
        /// <param name="newBid">Specifies to gets the nee bid amount</param>
        /// <returns>Awaitable task with no data</returns>
        Task UpdateBid(string productId, string email, int newBid);
    }
}
