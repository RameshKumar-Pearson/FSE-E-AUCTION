using BuyerApi.Exceptions;
using BuyerApi.Models;
using BuyerApi.RequestModels;
using BuyerApi.ResponseModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerApi.Validation
{
    /// <summary>
    /// Class used to implement <see cref="IBuyerValidation"/>
    /// </summary>
    public class BuyerValidation : IBuyerValidation
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<MongoBuyerResponse> _buyerCollection;
        private readonly DbConfiguration _settings;

        #region Public Methods

        /// <summary>
        ///  Constructor for <see cref="BuyerValidation"/>
        /// </summary>
        /// <param name="settings">Specifies to gets the <see cref="DbConfiguration"/></param>
        public BuyerValidation(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _buyerCollection = database.GetCollection<MongoBuyerResponse>(_settings.CollectionName);
            _productCollection = database.GetCollection<Product>("product_details");
        }

        ///<inheritdoc/>
        public async Task<bool> BusinessValidation(SaveBuyerRequestModel saveBuyerRequestModel)
        {
            var existingProducts = await GetProductsAsync();
            var productDetails = existingProducts.Where(x => x.Id == saveBuyerRequestModel.ProductId).Select(o => o).FirstOrDefault();
            if (productDetails == null)
            {
                throw new BuyerException("Product Id Is Not Exist");
            }
            else if (DateTime.Now > productDetails.BidEndDate)
            {
                throw new BuyerException("Bid End Date Is Over");
            }
            var existingBuyerList = await GetBuyerAsync();
            string existingBidAmount = existingBuyerList.Where(x => x.Email == saveBuyerRequestModel.Email && x.ProductId == saveBuyerRequestModel.ProductId).Select(o => o.BidAmount).FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(existingBidAmount) && existingBidAmount != "0")
            {
                throw new BuyerException("More than one bid on a product by same user (based on email ID) is not allowed");
            }

            return true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method used to gets the all list of existing product details
        /// </summary>
        /// <returns><see cref="List<<see cref="Product"/>>"/></returns>
        private async Task<List<Product>> GetProductsAsync()
        {
            return await _productCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///  Method used to gets the all list of existing buyer details
        /// </summary>
        /// <returns><see cref="List<<see cref="MongoBuyerResponse"/>>"/></returns>
        private async Task<List<MongoBuyerResponse>> GetBuyerAsync()
        {
            return await _buyerCollection.Find(c => true).ToListAsync();
        }

        #endregion
    }
}
