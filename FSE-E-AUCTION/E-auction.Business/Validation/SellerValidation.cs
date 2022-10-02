using E_auction.Business.Exception;
using E_auction.Business.Models;
using E_auction.Business.RequestModels;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace E_auction.Business.Validation
{
    /// <summary>
    /// Class used to do the business and other validations of product
    /// </summary>
    public class SellerValidation : ISellerValidation
    {
        private readonly IMongoCollection<MongoProduct> _productCollection;
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
            _productCollection = database.GetCollection<MongoProduct>("product_details");
            _buyerCollection = database.GetCollection<SaveBuyerRequestModel>("buyer_details");
        }

        ///<inheritdoc/>
        public async Task<bool> IsValidProduct(ProductDetails productDetails)
        {
            
            int startingPrice = Convert.ToInt32(productDetails.StartingPrice);

            var existingProducts = await _productCollection.Find<MongoProduct>(c => true).ToListAsync();

            var isProductExist = existingProducts.Where(x => x.StartingPrice == startingPrice && x.Name.Equals(productDetails.Name,StringComparison.OrdinalIgnoreCase) && x.Category.Equals(productDetails.Category, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (isProductExist != null)
            {
                throw new ProductException("Product already exist");
            }

            if (productDetails.BidEndDate < DateTime.Today.Date)
            {
                throw new SellerException(productDetails.BidEndDate);
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
        /// <returns><see cref="List<<see cref="MongoProduct"/>>"/></returns>
        private async Task<List<MongoProduct>> GetProductsAsync()
        {
            return await _productCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Method used to gets the all list of buyers details
        /// </summary>
        /// <returns><see cref="List<<see cref="MongoProduct"/>>"/></returns>
        private async Task<List<SaveBuyerRequestModel>> GetBuyersAsync()
        {
            return await _buyerCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
        }

        #endregion
    }
}
