using E_auction.Business.Models;
using E_auction.Business.Repositories;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace E_auction.Business.Directors
{
    /// <summary>
    /// Class used to manage the seller activities
    /// </summary>
    public class SellerDirector : ISellerDirector
    {
        private readonly ISellerRepository _sellerRepository;

        public SellerDirector(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }

        ///<inheritdoc/>
        public async Task<ProductResponse> AddProductAsync(ProductDetails productDetails)
        {
            return await _sellerRepository.AddProduct(productDetails);
        }

        ///<inheritdoc/>
        public async Task<bool> DeleteProductAsync(string ProductId)
        {
            await _sellerRepository.DeleteProductAsync(ProductId);
            return true;
        }
    }
}
