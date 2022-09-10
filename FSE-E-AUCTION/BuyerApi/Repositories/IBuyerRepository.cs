using BuyerApi.RequestModels;
using System.Threading.Tasks;

namespace BuyerApi.Repositories
{
    /// <summary>
    /// Interface class used to manage the buyer activities
    /// </summary>
    public interface IBuyerRepository
    {
        Task UpdateBid(string productId, string email, string newBid);
    }
}
