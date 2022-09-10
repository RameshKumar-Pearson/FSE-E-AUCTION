using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace BuyerApi
{
    public class KafkaConsumer
    {
        private readonly ConsumerConfig _consumerConfig;

        public KafkaConsumer(ConsumerConfig consumerConfig)
        {
            _consumerConfig = consumerConfig;
        }

        public void ConsumeKafkaMessage()
        {
            using (var consumer= new ConsumerBuilder<Null, string>(_consumerConfig).Build())
            {
                 consumer.Subscribe("eauction_buyer");
                while (true)
                {
                    var cr = consumer.Consume();
                    var data = cr.Message.Value;
                }
            }
        }
    }
}
