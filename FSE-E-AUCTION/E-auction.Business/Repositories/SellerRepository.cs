using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using E_auction.Business.Models;
using E_auction.Business.Context;
using E_auction.Business.Services.EmailService;

namespace E_auction.Business.Repositories
{
    /// <summary>
    /// Class used to manages the seller activities
    /// </summary>
    public class SellerRepository : ISellerRepository
    {
        private readonly IMongoCollection<MongoProduct> _productCollection;
        private readonly IMongoCollection<MongoSeller> _sellerCollection;
        private readonly EmailConfiguration _emailConfiguration;
        private readonly EmailSender _emailSender;

        /// <summary>
        /// Constructor for <see cref="SellerRepository"/>
        /// </summary>
        /// <param name="settings">Specifies to gets the db configuration</param>
        /// <param name="mongoDbContext">Specifies to gets the <see cref="MongoDbContext"/></param>
        /// <param name="emailConfiguration">Specifies to gets the <see cref="EmailConfiguration"/></param>
        /// <param name="emailSender"></param>
        public SellerRepository(IOptions<DbConfiguration> settings, IMongoDbContext mongoDbContext,
            IOptions<EmailConfiguration> emailConfiguration, IEmailSender emailSender)
        {
            var dbConfiguration = settings.Value;
            _productCollection = mongoDbContext.GetCollection<MongoProduct>("product_details");
            _sellerCollection = mongoDbContext.GetCollection<MongoSeller>(dbConfiguration.CollectionName);
            _emailConfiguration = emailConfiguration.Value;
            _emailSender = emailSender as EmailSender;
        }

        ///<inheritdoc/>
        public async Task<ProductResponse> AddProduct(ProductDetails productDetails)
        {
            var sellerDetails = new MongoSeller
            {
                FirstName = productDetails.FirstName,
                LastName = productDetails.LastName,
                Address = productDetails.Address,
                City = productDetails.City,
                State = productDetails.State,
                Email = productDetails.Email,
                Pin = productDetails.Pin,
                Phone = productDetails.Phone
            };

            await _sellerCollection.InsertOneAsync(sellerDetails).ConfigureAwait(false);

            var product = new MongoProduct
            {
                Name = productDetails.Name,
                ShortDescription = productDetails.ShortDescription,
                DetailedDescription = productDetails.DetailedDescription,
                Category = productDetails.Category,
                BidEndDate = productDetails.BidEndDate,
                StartingPrice = Convert.ToInt32(productDetails.StartingPrice),
                SellerId = sellerDetails.Id
            };

            await _productCollection.InsertOneAsync(product);

            return new ProductResponse { ProductId = product.Id, SellerId = product.SellerId };
        }

        ///<inheritdoc/>
        public async Task<DeleteResult> DeleteProductAsync(string productId)
        {
            
            var existingProducts = await _productCollection.Find(c => true).ToListAsync();

            var isExist = existingProducts.FirstOrDefault(x => x.Id.Equals(productId) && x.Id != null);

            if (isExist == null)
            {
                throw new System.Exception("Not found");
            }

            var deletedResult =
                await _productCollection.DeleteOneAsync(x => x.Id.Equals(productId)).ConfigureAwait(false);

            var sellerId = isExist.SellerId;

            var getExistingSellers = await _sellerCollection.Find(c => true).ToListAsync();

            var sellerDetails = getExistingSellers.FirstOrDefault(x => x.Id.Equals(sellerId) && x.Id != null);

            var message = new EmailMessage(
                new List<EmailAddress>
                {
                    new EmailAddress
                    {
                        DisplayName = sellerDetails?.FirstName + " " + sellerDetails?.LastName,
                        Address = sellerDetails?.Email
                    }
                }, _emailConfiguration.Subject,
                $"Hi {sellerDetails?.FirstName + " " + sellerDetails?.LastName} your product {isExist.Name} was deleted from the e_auction");

            _emailSender.SendEmail(message);

            return deletedResult;
        }

        ///<inheritdoc/>
        public async Task<List<ProductList>> GetProducts()
        {
            var existingProducts = await _productCollection.Find(c => true).ToListAsync();

            return existingProducts.Select(item => new ProductList { Id = item.Id, Name = item.Name }).ToList();
        }
    }
}
