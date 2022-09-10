using BuyerApi.Contracts.CommandHandlers;
using BuyerApi.Directors;
using BuyerApi.RequestModels;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly ProducerConfig _producerConfig;
        private readonly ConsumerConfig _consumerConfig;

        private readonly ISaveBuyerCommandHandler _saveBuyerCommandHandler;
        private readonly IBuyerDirector _buyerDirector;

        public BuyerController(IBuyerDirector buyerDirector, ISaveBuyerCommandHandler saveBuyerCommandHandler, ProducerConfig producerConfig, ConsumerConfig consumerConfig)
        {
            this._producerConfig = producerConfig;
            this._consumerConfig = consumerConfig;
            _buyerDirector = buyerDirector;
            _saveBuyerCommandHandler = saveBuyerCommandHandler;
        }

        [Route("place-bid")]
        [HttpPost]
        public async Task AddBid([FromBody] SaveBuyerRequestModel buyerDetails)
        {
            //Produce kafka message 
            await  PublishKafkaMessage("eauction_buyer", buyerDetails).ConfigureAwait(false);
           // await _saveBuyerCommandHandler.AddBid(buyerDetails);
        }

        [Route("update-bid/{productId}/{buyerEmailId}/{newBidAmount}")]
        [HttpPut]
        public Task UpdateBid(string productId,string buyerEmailId, string newBidAmount)
        {
            return _buyerDirector.UpdateBid(productId,buyerEmailId,newBidAmount);
        }

        #region private methods

        private async Task<IActionResult> PublishKafkaMessage(string topic, SaveBuyerRequestModel buyerDetails)
        {
            string seriealizedBuyerDetails = JsonConvert.SerializeObject(buyerDetails);
            try
            {
                using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
                {
                  await producer.ProduceAsync(topic, new Message<Null, string> { Value = seriealizedBuyerDetails }).ConfigureAwait(false);
                  producer.Flush(TimeSpan.FromSeconds(10));
                   await ConsumeKafkaMessage();
                  return Ok(true);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private async Task<IActionResult> ConsumeKafkaMessage()
        {
            using (var consumer = new ConsumerBuilder<Null, string>(_consumerConfig).Build())
            {
                consumer.Subscribe("eauction_buyer");
                while (true)
                {
                    var cr = consumer.Consume();
                    var data = cr.Message.Value;
                }
            }
        }

        #endregion 
    }
}
