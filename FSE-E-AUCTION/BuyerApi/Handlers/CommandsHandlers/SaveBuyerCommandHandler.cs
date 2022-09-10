using BuyerApi.Models;
using BuyerApi.RequestModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BuyerApi.Contracts.CommandHandlers;

namespace BuyerApi.Handlers.CommandsHandlers
{
    public class SaveBuyerCommandHandler : ISaveBuyerCommandHandler
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<SaveBuyerRequestModel> _buyerCollection;
        private readonly DbConfiguration _settings;

        public SaveBuyerCommandHandler(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _buyerCollection = database.GetCollection<SaveBuyerRequestModel>(_settings.CollectionName);
            _productCollection = database.GetCollection<Product>("Product_Details");
        }

        public async Task AddBid(SaveBuyerRequestModel buyerDetails)
        {
            var existingProducts = await GetProductsAsync();
            var productDetails = existingProducts.Where(x => x.Id == buyerDetails.ProductId).Select(o => o).FirstOrDefault();
            if (productDetails == null)
            {
                throw new Exception("Product Id Is Not Exist");
            }

            else if (DateTime.Now > productDetails.BidEndDate)
            {
                throw new Exception("Bid End Date Is Over");
            }

            var existingBuyerList = await GetBuyerAsync();
            string existingBidAmount = existingBuyerList.Where(x => x.Email == buyerDetails.Email && x.ProductId == buyerDetails.ProductId).Select(o => o.BidAmount).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(existingBidAmount) || existingBidAmount == "0")
            {
                await _buyerCollection.InsertOneAsync(buyerDetails);
            }

            throw new Exception("More than one bid on a product by same user (based on email ID) is not allowed");
        }

        #region

        private async Task<List<Product>> GetProductsAsync()
        {
            return await _productCollection.Find(c => true).ToListAsync();
        }
        private async Task<List<SaveBuyerRequestModel>> GetBuyerAsync()
        {
            return await _buyerCollection.Find(c => true).ToListAsync();
        }

        #endregion
    }
}
