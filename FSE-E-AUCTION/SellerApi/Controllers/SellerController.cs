using Azure.Messaging.ServiceBus.Administration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SellerApi.Contract.QueryHandlers;
using SellerApi.Directors;
using SellerApi.Exception;
using SellerApi.MessagePublishers;
using SellerApi.Models;
using SellerApi.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerApi.Controllers
{
    [Route("/e-auction/api/v1/seller")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerDirector _sellerDirector;
        private readonly IMessagePublisher _messagePublisher;
        private readonly IQueryHandler _iqueryHandler;
        private readonly ISellerValidation _isellerValidation;
        const string Topic = "e_auction";

        #region Public Methods

        /// <summary>
        /// constructor for <see cref="SellerController"/>
        /// </summary>
        /// <param name="sellerDirector">Specifies to gets the instance of <see cref="ISellerDirector"/></param>
        /// <param name="queryHandler">Specifies to gets the instance of <see cref="IQueryHandler"/></param>
        public SellerController(ISellerDirector sellerDirector, IQueryHandler queryHandler, ISellerValidation sellerValidation, IMessagePublisher messagePublisher)
        {
            _isellerValidation = sellerValidation;
            _iqueryHandler = queryHandler;
            _sellerDirector = sellerDirector;
            _messagePublisher = messagePublisher;
        }

        /// <summary>
        /// Method used to gets the bids of the product
        /// </summary>
        /// <param name="productId">Specifies to gets the the productId</param>
        /// <returns>List of bids</returns>
        [Route("show-bids/{productId}")]
        [HttpGet]
        public async Task<ProductBids> ShowBids(string productId)
        {
            return await _iqueryHandler.ShowBids(productId);
        }

        /// <summary>
        ///  Method used to add the new product 
        /// </summary>
        /// <param name="productDetails">Specifies to gets the product details</param>
        [Route("add-product")]
        [HttpPost]
        public async Task AddProductAsync([FromBody] ProductDetails productDetails)
        {
            if (await _isellerValidation.IsValidProduct(productDetails))
            {
                await _sellerDirector.AddProductAsync(productDetails);
            }
        }

        /// <summary>
        /// Method used to delete the product
        /// </summary>
        /// <param name="productId">Specifies to gets the productId</param>
        [Route("delete/{productId}")]
        [HttpDelete]
        public async Task<bool> Delete(string productId)
        {
            //if (await _isellerValidation.DeleteProductValidation(productId))
            //{
                await _messagePublisher.PublisherAsync(productId);
            //}
            return true;
        }

        #endregion
    }
}
