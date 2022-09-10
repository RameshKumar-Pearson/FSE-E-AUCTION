using BuyerApi.Contracts.CommandHandlers;
using BuyerApi.Repositories;
using BuyerApi.RequestModels;
using System;
using System.Threading.Tasks;

namespace BuyerApi.Directors
{
    /// <summary>
    /// Class used to manages the buyer activities
    /// </summary>
    public class BuyerDirector : IBuyerDirector
    {
        private readonly IBuyerRepository _buyerRepository;

        public BuyerDirector(IBuyerRepository buyerRepository)
        {
            _buyerRepository = buyerRepository;
        }

        public Task UpdateBid(string productId, string emailId, string newBid)
        {
            return _buyerRepository.UpdateBid(productId, emailId, newBid);
        }
    }
}
