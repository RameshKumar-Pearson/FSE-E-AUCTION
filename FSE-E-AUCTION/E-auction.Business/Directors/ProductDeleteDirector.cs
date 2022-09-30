using E_auction.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace E_auction.Business.Directors
{
    public class ProductDeleteDirector : IProductDeleteDirector
    {
        private readonly IProductDeleteRepository _deleteRepository;

        public ProductDeleteDirector(IProductDeleteRepository deleteRepository)
        {
            _deleteRepository = deleteRepository;
        }

        ///<inheritdoc/>
        public async Task<bool> DeleteProductAsync(string ProductId)
        {
            await _deleteRepository.DeleteProductAsync(ProductId);
            return true;
        }
    }
}
