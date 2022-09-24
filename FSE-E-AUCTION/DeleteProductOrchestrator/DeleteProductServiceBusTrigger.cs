using System;
using System.Threading.Tasks;
using DeleteProductOrchestrator.Directors;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DeleteProductOrchestrator
{
    public class DeleteProductServiceBusTrigger
    {
        private readonly IProductDeleteDirector _productDeleteDirector;

        public DeleteProductServiceBusTrigger(IProductDeleteDirector productDeleteDirector)
        {
            _productDeleteDirector = productDeleteDirector;
        }

        [FunctionName(nameof(DeleteProductServiceBusTrigger))]
        public async Task Run([ServiceBusTrigger("e_auction", "product_delete", Connection = "AzureWebJobsServiceBus")] string productId, ILogger logger)
        {
            try
            {
                logger.LogInformation($"DeleteProductServiceBusTrigger started with the productId: {productId}");
                await _productDeleteDirector.DeleteProductAsync(productId);
                logger.LogInformation($"DeleteProductServiceBusTrigger completed for the productId: {productId}");
            }
            catch(Exception ex)
            {
                logger.LogInformation($"DeleteProductServiceBusTrigger completed with error for the productId: {productId} exception message:{ex.Message} and stackTrace:{ex.StackTrace} and InnerException:{ex.InnerException}");
                throw;
            }
        }
    }
}
