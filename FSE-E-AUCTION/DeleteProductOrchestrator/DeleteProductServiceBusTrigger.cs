using System;
using System.Threading.Tasks;
using E_auction.Business.Directors;
using E_auction.Business.Models;
using E_auction.Business.Repositories;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DeleteProductOrchestrator
{
    /// <summary>
    /// service bus trigger for deleting the product
    /// </summary>
    public class DeleteProductServiceBusTrigger
    {
        private readonly ISellerDirector _sellerDirector;
        private readonly ISellerRepository _sellerRepository;

        /// <summary>
        /// constructor for <see cref="DeleteProductServiceBusTrigger"/>
        /// </summary>
        /// <param name="sellerDirector">Specifies to gets <see cref="ISellerDirector"/></param>
        public DeleteProductServiceBusTrigger(IOptions<DbConfiguration> options)
        {
            _sellerRepository = new SellerRepository(options);
            _sellerDirector = new SellerDirector(_sellerRepository);
        }

        [FunctionName(nameof(DeleteProductServiceBusTrigger))]
        public async Task Run([ServiceBusTrigger("e_auction_delete", "product_delete", Connection = "AzureWebJobsServiceBus")] string productId, ILogger logger)
        {
            try
            {
                logger.LogInformation($"DeleteProductServiceBusTrigger started with the productId: {productId}");

                var response=  await _sellerDirector.DeleteProductAsync(productId);

                logger.LogInformation($"DeleteProductServiceBusTrigger completed for the productId: {productId} and Response:{ JsonConvert.SerializeObject(response)}");
            }
            catch(Exception ex)
            {
                logger.LogInformation($"DeleteProductServiceBusTrigger completed with error for the productId: {productId} exception message:{ex.Message} and stackTrace:{ex.StackTrace} and InnerException:{ex.InnerException}");
                throw;
            }
        }
    }
}
