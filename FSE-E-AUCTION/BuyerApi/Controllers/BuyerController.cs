using BuyerApi.Directors;
using BuyerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerApi.Controllers
{
    [Route("/e-auction/api/v1/buyer")]
    [ApiController]
    public class BuyerController : Controller
    {
        private readonly IBuyerDirector _buyerDirector;

        public BuyerController(IBuyerDirector buyerDirector)
        {
            _buyerDirector = buyerDirector;
        }

        [Route("place-bid")]
        [HttpPost]
        public async Task AddBid([FromBody] BuyerDetails buyerDetails)
        {
            await _buyerDirector.AddBid(buyerDetails);
        }

        [Route("update-bid/{productId}/{buyerEmailId}/{newBidAmount}")]
        [HttpPut]
        public async Task<string> UpdateBid(string productId,string buyerEmailId, string newBidAmount)
        {
            return "updated";
        }

    }
}
