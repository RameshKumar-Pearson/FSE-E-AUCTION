using System;
using System.Threading.Tasks;
using E_auction.Business.Contract.CommandHandlers;
using E_auction.Business.RequestModels;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EauctionBidEventHandler
{
    public class BidHandler
    {
       
        private readonly ISaveBuyerCommandHandler _saveBuyerCommandHandler;

        public BidHandler( ISaveBuyerCommandHandler saveBuyerCommandHandler)
        {
            _saveBuyerCommandHandler = saveBuyerCommandHandler;
        }

        [FunctionName(nameof(BidHandler))]
        public async Task Run([ServiceBusTrigger("e_auction_bid", "product_addbid", Connection = "AzureWebJobsServiceBus")] SaveBuyerRequestModel saveBuyerRequest, ILogger logger)
        {
            try
            {
                logger.LogInformation($"ProductAddBidServiceBusTrigger started with the productdetails: {JsonConvert.SerializeObject(saveBuyerRequest)}");

                await _saveBuyerCommandHandler.AddBid(saveBuyerRequest);

                logger.LogInformation($"ProductAddBidServiceBusTrigger completed for the productId: {JsonConvert.SerializeObject(saveBuyerRequest)}");
            }
            catch (Exception ex)
            {
                logger.LogInformation($"ProductAddBidServiceBusTrigger completed with error for the productId: {saveBuyerRequest} exception message:{ex.Message} and stackTrace:{ex.StackTrace} and InnerException:{ex.InnerException}");
                throw;
            }
        }
    }
}