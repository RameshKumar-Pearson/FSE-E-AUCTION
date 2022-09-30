using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace E_auction.Business.Repositories
{
    /// <summary>
    /// Interface class used to delete the product
    /// </summary>
    public interface IProductDeleteRepository
    {
        /// <summary>
        /// Method used to deletes the product
        /// </summary>
        /// <param name="ProductId">Specifies to gets the productId</param>
        /// <returns></returns>
        Task<bool> DeleteProductAsync(string ProductId);
    }
}
