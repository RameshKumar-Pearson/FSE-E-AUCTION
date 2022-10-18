using E_auction.Business.Repositories;
using System.Threading.Tasks;

namespace E_auction.Business.Directors
{
    /// <summary>
    /// Director Class used to manages the buyer activities
    /// </summary>
    public class BuyerDirector : IBuyerDirector
    {
        private readonly IBuyerRepository _buyerRepository;

        #region Public Methods

        /// <summary>
        /// Constructor for <see cref="BuyerDirector"/>
        /// </summary>
        /// <param name="buyerRepository">Specifies to gets the instance of <see cref="IBuyerRepository"/></param>
        public BuyerDirector(IBuyerRepository buyerRepository)
        {
            _buyerRepository = buyerRepository;
        }

        ///<inheritdoc/>
        public Task UpdateBid(string productId, string emailId, int newBid)
        {
            return _buyerRepository.UpdateBid(productId, emailId, newBid);
        }

        #endregion
    }
}