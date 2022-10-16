
using E_auction.Business.Models;
using System.Threading.Tasks;

namespace E_auction.Business.Validation
{
    /// <summary>
    /// Interface class used to do the validations of seller
    /// </summary>
    public interface ISellerValidation
    {
        /// <summary>
        /// Method used to do the business validation of product
        /// </summary>
        /// <param name="productDetails">Specifies to gets the <see cref="ProductDetails"/></param>
        /// <returns>If the validation is successfully done it returns true, otherwise false </returns>
        Task<bool> IsValidProduct(ProductDetails productDetails);

        /// <summary>
        /// Method used to delete the product
        /// </summary>
        /// <param name="productId">Specifies to gets the productId</param>
        /// <returns>If the validation is successfully done it returns true, otherwise false</returns>
        Task<bool> DeleteProductValidation(string productId);
    }
}
