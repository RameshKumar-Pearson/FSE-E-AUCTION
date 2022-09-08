using BuyerApi.Models;
using System.Threading.Tasks;

namespace BuyerApi.Directors
{
    /// <summary>
    /// Interface class used to manages the buyer activities
    /// </summary>
    public interface IBuyerDirector
    {
        Task AddBid(BuyerDetails buyerDetails);

        Task UpdateBid(string productId, string email, int newBid);
    }
}
