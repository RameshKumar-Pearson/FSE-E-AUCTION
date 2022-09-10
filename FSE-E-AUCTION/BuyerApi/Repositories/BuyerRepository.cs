﻿using BuyerApi.Models;
using BuyerApi.RequestModels;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuyerApi.Repositories
{
    /// <summary>
    /// Class used to manages the repository activities
    /// </summary>
    public class BuyerRepository : IBuyerRepository
    {
        private readonly IMongoCollection<SaveBuyerRequestModel> _buyerCollection;
        private readonly DbConfiguration _settings;

        public BuyerRepository(IOptions<DbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _buyerCollection = database.GetCollection<SaveBuyerRequestModel>(_settings.CollectionName);
        }

        public async Task UpdateBid(string productId, string email, string newBid)
        {
            var buyersList = await GetBuyerAsync();
            var buyerDetails = buyersList.Where(x => x.Email == email && x.ProductId == productId).Select(o => o).FirstOrDefault();
            if (buyerDetails != null)
            {
                buyerDetails.BidAmount = newBid;
            }

            await _buyerCollection.ReplaceOneAsync(x => x.Id == buyerDetails.Id, buyerDetails);
        }

        #region

        private async Task<List<SaveBuyerRequestModel>> GetBuyerAsync()
        {
            return await _buyerCollection.Find(c => true).ToListAsync();
        }

        #endregion
    }
}
