using BuyerApi.RequestModels;
using System.Threading.Tasks;

namespace BuyerApi.Contracts.CommandHandlers
{
    /// <summary>
    /// Interface class used to manages the buyer activities
    /// </summary>
    public interface ISaveBuyerCommandHandler
    {
        Task AddBid(SaveBuyerRequestModel buyerRequest);
    }
}
