using BuyerApi.Models;
using System.Threading.Tasks;

namespace BuyerApi.Directors
{
    /// <summary>
    /// Interface class used to manages the buyer activities
    /// </summary>
    public interface IBuyerDirector
    {
        Task UpdateBid(string productId, string email, string newBid);
    }
}
