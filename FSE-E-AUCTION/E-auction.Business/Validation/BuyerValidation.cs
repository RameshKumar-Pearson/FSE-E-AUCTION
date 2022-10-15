
using E_auction.Business.Exception;
using E_auction.Business.Models;
using E_auction.Business.ResponseModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaveBuyerRequestModel = E_auction.Business.RequestModels.SaveBuyerRequestModel;

namespace E_auction.Business.Validation
{
    /// <summary>
    /// Class used to implement <see cref="IBuyerValidation"/>
    /// </summary>
    public class BuyerValidation : IBuyerValidation
    {
        private readonly IMongoCollection<MongoProduct> _productCollection;
        private readonly IMongoCollection<MongoBuyerResponse> _buyerCollection;

        #region Public Methods

        /// <summary>
        ///  Constructor for <see cref="BuyerValidation"/>
        /// </summary>
        /// <param name="settings">Specifies to gets the <see cref="DbConfiguration"/></param>
        public BuyerValidation(IOptions<DbConfiguration> settings)
        {
            var configurationValue = settings.Value;
            var client = new MongoClient(configurationValue.ConnectionString);
            var database = client.GetDatabase(configurationValue.DatabaseName);
            _buyerCollection = database.GetCollection<MongoBuyerResponse>(configurationValue.CollectionName);
            _productCollection = database.GetCollection<MongoProduct>("product_details");
        }

        ///<inheritdoc/>
        public async Task<bool> BusinessValidation(SaveBuyerRequestModel saveBuyerRequestModel)
        {
            var existingProducts = await GetProductsAsync();
            var productDetails = existingProducts.Where(x => x.Id == saveBuyerRequestModel.ProductId).Select(o => o).FirstOrDefault();
            if (productDetails == null)
            {
                throw new BuyerException("Product Doe's Not Exist");
            }
            else if (DateTime.Now > productDetails.BidEndDate)
            {
                throw new BuyerException("Bid End Date Is Already Over");
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
        /// <returns><see cref="MongoProduct"/></returns>
        private async Task<List<MongoProduct>> GetProductsAsync()
        {
            return await _productCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        ///  Method used to gets the all list of existing buyer details
        /// </summary>
        /// <returns><see cref="MongoBuyerResponse"/></returns>
        private async Task<List<MongoBuyerResponse>> GetBuyerAsync()
        {
            return await _buyerCollection.Find(c => true).ToListAsync();
        }

        #endregion
    }
}
