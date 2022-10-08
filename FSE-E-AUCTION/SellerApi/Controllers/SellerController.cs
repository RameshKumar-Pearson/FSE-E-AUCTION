using Azure.Messaging.ServiceBus.Administration;
using E_auction.Business.Contract.QueryHandlers;
using E_auction.Business.Directors;
using E_auction.Business.MessagePublishers;
using E_auction.Business.Models;
using E_auction.Business.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        private readonly ILogger _logger;

        #region Public Methods

        /// <summary>
        /// constructor for <see cref="SellerController"/>
        /// </summary>
        /// <param name="sellerDirector">Specifies to gets the instance of <see cref="ISellerDirector"/></param>
        /// <param name="queryHandler">Specifies to gets the instance of <see cref="IQueryHandler"/></param>
        public SellerController(ISellerDirector sellerDirector, IQueryHandler queryHandler, ISellerValidation sellerValidation, IMessagePublisher messagePublisher, ILoggerFactory loggerFactory)
        {
            _isellerValidation = sellerValidation;
            _iqueryHandler = queryHandler;
            _sellerDirector = sellerDirector;
            _messagePublisher = messagePublisher;
            _logger = loggerFactory.CreateLogger<SellerController>();
        }

        /// <summary>
        /// Method used to gets the bids of the product
        /// </summary>
        /// <param name="productId">Specifies to gets the the productId</param>
        /// <returns>List of bids</returns>
        [Route("show-bids/{productId}")]
        [HttpGet]
        public async Task<IActionResult> ShowBids(string productId)
        {
            _logger.LogInformation($"Show bids for the product {productId} started");

            if (string.IsNullOrWhiteSpace(productId))
            {
                return BadRequest("Product Id Should Not Be Empty");
            }

            var response = await _iqueryHandler.ShowBids(productId);

            _logger.LogInformation($"Show bids for the{productId} product completed");

            return Ok(response);
        }

        /// <summary>
        ///  Method used to add the new product 
        /// </summary>
        /// <param name="productDetails">Specifies to gets the product details</param>
        [Route("add-product")]
        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] ProductDetails productDetails)
        {
            _logger.LogInformation($"Add product started");

            ProductResponse response = new ProductResponse();

            if (!Regex.IsMatch(productDetails.StartingPrice, @"^\d+$"))
            {
                return BadRequest("Invalid Price, Price Should Be Valid Number");
            }
          
            string[] stringArray = { "Painting", "Sculptor", "Ornament" };
        
            if(!stringArray.Contains(productDetails.Category, StringComparer.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid product category, Product category must be one of the following : Painting, Sculptor, Ornament");
            }

            if (await _isellerValidation.IsValidProduct(productDetails))
            {
                response = await _sellerDirector.AddProductAsync(productDetails);
            }

            _logger.LogInformation($"Product successfully added");

            return Ok(response);
        }

        /// <summary>
        /// Method used to delete the product
        /// </summary>
        /// <param name="productId">Specifies to gets the productId</param>
        [Route("delete/{productId}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string productId)
        {
            _logger.LogInformation($"Delete product started for the product {productId}");

             await _messagePublisher.PublisherAsync(productId);

            _logger.LogInformation($"Delete product completed for the product {productId}");

            return Ok("Delete product event raised successfully");
        }

        #endregion
    }
}
