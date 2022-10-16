using System.Collections.Generic;
using E_auction.Business.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace E_auction.Business.Directors
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
        Task<ProductResponse> AddProductAsync(ProductDetails productDetails);

        /// <summary>
        /// Method used to deletes the product
        /// </summary>
        /// <param name="productId">Specifies to gets the productId</param>
        /// <returns></returns>
        Task<DeleteResult> DeleteProductAsync(string productId);

        /// <summary>
        /// Method used to gets the list of products
        /// </summary>
        /// <returns>Awaitable task with <see cref="ProductList"/></returns>
        Task<List<ProductList>> GetProducts();
    }
}
