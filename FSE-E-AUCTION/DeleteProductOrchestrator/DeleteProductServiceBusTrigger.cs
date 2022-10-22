using System;
using System.Threading.Tasks;
using E_auction.Business.Directors;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EauctionDeleteEventHandler
{
    /// <summary>
    ///     service bus trigger for deleting the product
    /// </summary>
    public class DeleteProductServiceBusTrigger
    {
        private readonly ISellerDirector _sellerDirector;

        /// <summary>
        ///     Constructor for <see cref="DeleteProductServiceBusTrigger" />
        /// </summary>
        /// <param name="sellerDirector">Specifies to gets <see cref="ISellerDirector" /></param>
        public DeleteProductServiceBusTrigger(ISellerDirector sellerDirector)
        {
            _sellerDirector = sellerDirector;
        }

        /// <summary>
        ///     Trigger method for delete service bus trigger
        /// </summary>
        /// <param name="productId">Specifies to gets the productId</param>
        /// <param name="logger">Specifies to gets the <see cref="ILogger" /></param>
        /// <returns>Awaitable task with no data</returns>
        [FunctionName(nameof(DeleteProductServiceBusTrigger))]
        public async Task Run(
            [ServiceBusTrigger("e_auction_delete", "product_delete", Connection = "AzureWebJobsServiceBus")]
            string productId, ILogger logger)
        {
            try
            {
                logger.LogInformation($"{nameof(DeleteProductServiceBusTrigger)} started with the productId: {productId}");

                productId = JsonConvert.DeserializeObject<string>(productId);

                var response = await _sellerDirector.DeleteProductAsync(productId);

                logger.LogInformation(
                    $"{nameof(DeleteProductServiceBusTrigger)} completed for the productId: {productId} and Response:{JsonConvert.SerializeObject(response)}");
            }
            catch (Exception ex)
            {
                logger.LogInformation(
                    $" completed with error for the productId: {productId} exception message:{ex.Message} and stackTrace:{ex.StackTrace} and InnerException:{ex.InnerException}");
                throw;
            }
        }
    }
}