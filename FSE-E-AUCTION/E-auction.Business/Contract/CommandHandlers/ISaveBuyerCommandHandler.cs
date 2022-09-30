using E_auction.Business.RequestModels;
using E_auction.Business.ResponseModels;
using System.Threading.Tasks;

namespace E_auction.Business.Contract.CommandHandlers
{
    /// <summary>
    /// Interface class used to manages the buyer activities
    /// </summary>
    public interface ISaveBuyerCommandHandler
    {
        /// <summary>
        /// Method used to add bid to the database 
        /// </summary>
        /// <param name="buyerRequest">Specifies to gets the <see cref="MongoBuyerResponse"/></param>
        /// <returns>Awaitable task with no data</returns>
        Task AddBid(SaveBuyerRequestModel buyerRequest);
    }
}
