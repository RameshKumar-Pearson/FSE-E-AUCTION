using Confluent.Kafka;
using E_auction.Business.Contract.CommandHandlers;
using E_auction.Business.Directors;
using E_auction.Business.Handlers.CommandsHandlers;
using E_auction.Business.MessagePublishers;
using E_auction.Business.Models;
using E_auction.Business.RequestModels;
using E_auction.Business.Validation;
using MassTransit;
using MassTransit.KafkaIntegration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;
        private readonly ISaveBuyerCommandHandler _saveBuyerCommandHandler;
       

        /// <summary>
        /// Constructor for <see cref="BuyerController"/>
        /// </summary>
        /// <param name="buyerDirector">Specifies to gets the object instance for <see cref="IBuyerDirector"/></param>
        /// <param name="topicProducer">Specifies to gets the object instance for <see cref="ITopicProducer<<see cref="KafkaBuyerEventCreate"/>></param>
        public BuyerController(IBuyerDirector buyerDirector, ITopicProducer<KafkaBuyerEventCreate> topicProducer, IBuyerValidation buyerValidation, ILoggerFactory loggerFactory, IOptions<DbConfiguration> settings)
        {
            _buyerValidation = buyerValidation;
            _buyerDirector = buyerDirector;
            _topicProducer = topicProducer;
            _logger = loggerFactory.CreateLogger<BuyerController>();
            _saveBuyerCommandHandler = new SaveBuyerCommandHandler(settings);
        }

        #region Public Methods

        /// <summary>
        /// Method used to add the bid amount for existing product
        /// </summary>
        /// <param name="buyerDetails">Specifies to gets <see cref="MongoBuyerResponse"/></param>
        /// <returns>Awaitable task with no data</returns>
        [Route("place-bid")]
        [HttpPost]
        public async Task<IActionResult> AddBid([FromBody] SaveBuyerRequestModel buyerDetails)
        {
            _logger.LogInformation($"Add bid to the product started");
            if (await _buyerValidation.BusinessValidation(buyerDetails))
            {
                //TODO: Some deployment issue is happen while raising kafka event(code implemented) so we needs to fix in the upcoming days .. So as of now we are raising the event to service bus trigger..
                // await PublishKafkaMessage("eauction_buyer", buyerDetails);

                await _saveBuyerCommandHandler.AddBid(buyerDetails);
            }
            _logger.LogInformation($"Add bid to the product completed");

            return Ok("Add bid to the product started");
        }

        [Route("update-bid/{productId}/{buyerEmailId}/{newBidAmount}")]
        [HttpPut]
        public Task UpdateBid(string productId, string buyerEmailId, string newBidAmount)
        {
            _logger.LogInformation($"Update bid for the product started{productId}");

             return _buyerDirector.UpdateBid(productId, buyerEmailId, newBidAmount);

            _logger.LogInformation($"Update bid for the product completed{productId}");
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
