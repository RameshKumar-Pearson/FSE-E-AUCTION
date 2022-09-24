using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeleteProductOrchestrator.Repositories
{
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
