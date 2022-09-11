using SellerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerApi.Contract.QueryHandlers
{
    /// <summary>
    ///  CQRS Query Handler used to get all the bids for the existing product
    /// </summary>
    public interface IQueryHandler
    {
        /// <summary>
        /// Method used to gets the bids of the product
        /// </summary>
        /// <param name="ProductId">Specifies to gets the productId</param>
        /// <returns>Awaitable task with no data</returns>
        Task<ProductBids> ShowBids(string ProductId);
    }
}
