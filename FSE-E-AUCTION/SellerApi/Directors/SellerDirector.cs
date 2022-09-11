using MongoDB.Driver;
using SellerApi.Models;
using SellerApi.Repositories;
using System.Threading.Tasks;

namespace SellerApi.Directors
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
        public async Task AddProductAsync(ProductDetails productDetails)
        {
            await _sellerRepository.AddProduct(productDetails);
        }

        ///<inheritdoc/>
        public async Task<bool> DeleteProductAsync(string ProductId)
        {
            await _sellerRepository.DeleteProductAsync(ProductId);
            return true;
        }
    }
}
