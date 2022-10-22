using E_auction.Business.Models;
using System.Threading.Tasks;

namespace E_auction.Business.Contract.QueryHandlers
{
    /// <summary>
    ///  CQRS Query Handler used to get all the bids for the existing product
    /// </summary>
    public interface IQueryHandler
    {
        /// <summary>
        /// Method used to gets the bids of the product
        /// </summary>
        /// <param name="productId">Specifies to gets the productId</param>
        /// <returns>Awaitable task with no data</returns>
        Task<ProductBids> ShowBidsAsync(string productId);
    }
}
