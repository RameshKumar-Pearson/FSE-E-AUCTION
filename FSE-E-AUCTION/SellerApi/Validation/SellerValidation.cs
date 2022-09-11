using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SellerApi.Exception;
using SellerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerApi.Validation
{
    /// <summary>
    /// Class used to do the business and other validations of product
    /// </summary>
    public class SellerValidation : ISellerValidation
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<SaveBuyerRequestModel> _buyerCollection;
        private readonly DbConfiguration _settings;

        #region Public Methods

        /// <summary>
        /// Construtor for <see cref="SellerValidation"/>
        /// </summary>
        /// <param name="settings"></param>
        public SellerValidation(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _productCollection = database.GetCollection<Product>("Product_Details");
            _buyerCollection = database.GetCollection<SaveBuyerRequestModel>("Buyer_Details");
        }

        ///<inheritdoc/>
        public async Task<bool> AddProductValidation(ProductDetails productDetails)
        {
            if (productDetails.Details.BidEndDate < System.DateTime.Today.Date)
            {
                throw new SellerException(productDetails.Details.BidEndDate);
            }

            string[] stringArray = { "Painting", "Sculptor", "Ornament" };
            int pos = Array.IndexOf(stringArray, productDetails.Details.Category);
            if (!(pos > -1))
            {
                throw new System.Exception("Invalid product category");
            }

            return true;
        }

        ///<inheritdoc/>
        public async Task<bool> DeleteProductValidation(string productId)
        {
            var productList = await GetProductsAsync();
            var buyerList = await GetBuyersAsync();

            var bidEndDate = productList.Where(x => x.Id == productId).Select(o => o.BidEndDate).FirstOrDefault();

            if (DateTime.Now > bidEndDate)
            {
                throw new SellerException("BidEndDate was expired");
            }

            var bidAmount = buyerList.Where(x => x.ProductId == productId).Select(o => o.BidAmount).ToList();

            if (bidAmount.Count > 0)
            {
                throw new SellerException("You can not delete this product since its having bids amount");
            }

            return true;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Method used to gets the all list of existing product details
        /// </summary>
        /// <returns><see cref="List<<see cref="Product"/>>"/></returns>
        private async Task<List<Product>> GetProductsAsync()
        {
            return await _productCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Method used to gets the all list of buyers details
        /// </summary>
        /// <returns><see cref="List<<see cref="Product"/>>"/></returns>
        private async Task<List<SaveBuyerRequestModel>> GetBuyersAsync()
        {
            return await _buyerCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
        }

        #endregion
    }
}
