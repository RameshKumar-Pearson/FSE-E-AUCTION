using BuyerApi.Contracts.CommandHandlers;
using BuyerApi.Directors;
using BuyerApi.Models;
using BuyerApi.RequestModels;
using BuyerApi.ResponseModels;
using BuyerApi.Validation;
using Confluent.Kafka;
using MassTransit;
using MassTransit.KafkaIntegration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerApi.Controllers
{
    /// <summary>
    /// Controller used to manages the buyer opearations
    /// </summary>
    [Route("/e-auction/api/v1/buyer")]
    [ApiController]
    public class BuyerController : Controller
    {
        private readonly ITopicProducer<KafkaBuyerEventCreate> _topicProducer;
        private readonly IBuyerDirector _buyerDirector;
        private readonly IBuyerValidation _buyerValidation;

        /// <summary>
        /// Constructor for <see cref="BuyerController"/>
        /// </summary>
        /// <param name="buyerDirector">Specifies to gets the object instance for <see cref="IBuyerDirector"/></param>
        /// <param name="topicProducer">Specifies to gets the object instance for <see cref="ITopicProducer<<see cref="KafkaBuyerEventCreate"/>></param>
        public BuyerController(IBuyerDirector buyerDirector, ITopicProducer<KafkaBuyerEventCreate> topicProducer, IBuyerValidation buyerValidation)
        {
            _buyerValidation = buyerValidation;
            _buyerDirector = buyerDirector;
            _topicProducer = topicProducer;
        }

        #region Public Methods

        /// <summary>
        /// Method used to add the bid amount for existing product
        /// </summary>
        /// <param name="buyerDetails">Specifies to gets <see cref="MongoBuyerResponse"/></param>
        /// <returns>Awaitable task with no data</returns>
        [Route("place-bid")]
        [HttpPost]
        public async Task AddBid([FromBody] SaveBuyerRequestModel buyerDetails)
        {
            if (await _buyerValidation.BusinessValidation(buyerDetails))
            {
                //Produce kafka message with buyer details
                await PublishKafkaMessage("eauction_buyer", buyerDetails);
            }
        }

        [Route("update-bid/{productId}/{buyerEmailId}/{newBidAmount}")]
        [HttpPut]
        public Task UpdateBid(string productId, string buyerEmailId, string newBidAmount)
        {
            return _buyerDirector.UpdateBid(productId, buyerEmailId, newBidAmount);
        }

        #endregion

        #region private methods

        /// <summary>
        /// Method used to raise the event in kafka with the respective buyer topic with message..
        /// </summary>
        /// <param name="topic">Specifies to gets the topic name</param>
        /// <param name="buyerDetails">Specifies to gets the <see cref="MongoBuyerResponse"/></param>
        /// <returns><see cref="IActionResult"/></returns>
        private async Task<IActionResult> PublishKafkaMessage(string topic, SaveBuyerRequestModel buyerDetails)
        {
            await _topicProducer.Produce(new KafkaBuyerEventCreate
            {
                Topic = $"{topic}",
                TopicMessage = buyerDetails
            });
            return Ok(true);
        }

        #endregion 
    }
}
