using System;
using System.Collections.Generic;
using Confluent.Kafka;
namespace KafkaEvents
{
    public class BuyerConsumer : IBuyerConsumer
    {
        public void Listen(Func<string> message)
        {
            var config = new Dictionary<>
            throw new NotImplementedException();
        }
    }
}
