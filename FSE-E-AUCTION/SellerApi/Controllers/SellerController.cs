using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SellerApi.Directors;
using SellerApi.Exception;
using SellerApi.Models;
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

        public SellerController(ISellerDirector sellerDirector)
        {
            _sellerDirector = sellerDirector;
        }

        /// <summary>
        /// Method used to gets the bids of the product
        /// </summary>
        /// <param name="productId">Specifies to gets the the productId</param>
        /// <returns>List of bids</returns>
        [Route("show-bids/{productId}")]
        [HttpGet]
        public async Task<Product> ShowBids(string productId)
        {
            return await _sellerDirector.ShowBidsAsync(productId);
        }

        /// <summary>
        ///  Method used to add the new product 
        /// </summary>
        /// <param name="productDetails">Specifies to gets the product details</param>
        [Route("add-product")]
        [HttpPost]
        public async Task AddProductAsync([FromBody] ProductDetails productDetails)
        {
            if (productDetails.Details.BidEndDate < System.DateTime.Today.Date)
            {
                throw new InvalidBidDateException(productDetails.Details.BidEndDate);
            }

            string[] stringArray = { "Painting", "Sculptor", "Ornament" };
            int pos = Array.IndexOf(stringArray, productDetails.Details.Category);
            if (!(pos > -1))
            {
                throw new System.Exception("Invalid product category");
            }

            await _sellerDirector.AddProductAsync(productDetails);
        }

        /// <summary>
        /// Method used to delete the product
        /// </summary>
        /// <param name="productId">Specifies to gets the productId</param>
        [Route("delete/{productId}")]
        [HttpDelete]
        public async Task<DeleteResult> Delete(string productId)
        {
           return await _sellerDirector.DeleteProductAsync(productId);
        }
    }
}
