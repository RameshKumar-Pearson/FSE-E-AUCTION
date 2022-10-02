﻿
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;
using Microsoft.Extensions.Options;
using System;
using E_auction.Business.Models;
using E_auction.Business.RequestModels;

namespace E_auction.Business.Repositories
{
    /// <summary>
    /// Class used to manages the seller activities
    /// </summary>
    public class SellerRepository : ISellerRepository
    {
        private readonly IMongoCollection<SaveBuyerRequestModel> _buyerCollection;
        private readonly IMongoCollection<MongoProduct> _productCollection;
        private readonly IMongoCollection<MongoSeller> _sellerCollection;
        private readonly DbConfiguration _settings;

        /// <summary>
        /// Constructor for <see cref="SellerRepository"/>
        /// </summary>
        /// <param name="settings">Specifies to gets the db configuration</param>
        public SellerRepository(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _buyerCollection = database.GetCollection<SaveBuyerRequestModel>("Buyer_Details");
            _productCollection = database.GetCollection<MongoProduct>("product_details");
            _sellerCollection = database.GetCollection<MongoSeller>(_settings.CollectionName);
        }

        ///<inheritdoc/>
        public async Task<ProductResponse> AddProduct(ProductDetails productDetails)
        {
            var sellerDetails = new MongoSeller
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                FirstName = productDetails.FirstName,
                LastName = productDetails.LastName,
                Address = productDetails.Address,
                City = productDetails.City,
                State = productDetails.State,
                Email = productDetails.Email,
                Pin = productDetails.Pin,
                Phone = productDetails.Phone
            };

            var product = new MongoProduct
            {
                Name = productDetails.Name,
                ShortDescription = productDetails.ShortDescription,
                DetailedDescription = productDetails.DetailedDescription,
                Category = productDetails.Category,
                BidEndDate = productDetails.BidEndDate,
                StartingPrice= Convert.ToInt32(productDetails.StartingPrice),
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                SellerId = sellerDetails.Id
            };

            await _sellerCollection.InsertOneAsync(sellerDetails).ConfigureAwait(false);

            await _productCollection.InsertOneAsync(product);

            return new ProductResponse { ProductId = product.Id, SellerId = product.SellerId };
        }

        ///<inheritdoc/>
        public async Task<bool> DeleteProductAsync(string ProductId)
        {
            await _productCollection.DeleteOneAsync(c => c.Id == ProductId);
            return true;
        }
    }
}