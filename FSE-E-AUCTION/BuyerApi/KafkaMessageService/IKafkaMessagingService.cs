using BuyerApi.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerApi.KafkaMessageService
{
   public interface IKafkaMessagingService
    {
        bool ProduceMessage(string topic, SaveBuyerRequestModel buyerDetails);

        Task ConsumeMessage();
    }
}
