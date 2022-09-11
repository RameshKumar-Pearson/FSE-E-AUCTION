using MongoDB.Driver;
using SellerApi.Models;
using System.Threading.Tasks;

namespace SellerApi.Directors
{
    /// <summary>
    /// Interface class used to manage the seller activities
    /// </summary>
    public interface ISellerDirector
    {
        /// <summary>
        /// Method used to add the new products
        /// </summary>
        /// <param name="productDetails">Specifies to gets the product details</param>
        /// <returns>Awaitable task with no data</returns>
        Task AddProductAsync(ProductDetails productDetails);

        /// <summary>
        /// Method used to deletes the product
        /// </summary>
        /// <param name="ProductId">Specifies to gets the productId</param>
        /// <returns></returns>
        Task<bool> DeleteProductAsync(string ProductId);
    }
}
