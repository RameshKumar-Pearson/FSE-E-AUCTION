using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerApi.MessagePublishers
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly ITopicClient _topicClient;
        public MessagePublisher(ITopicClient topicClient)
        {
            _topicClient = topicClient;
        }
        public async Task PublisherAsync<T>(T request)
        {
           var message = new Message
            {
                MessageId = Guid.NewGuid().ToString(),
                Body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request))
            };
            message.UserProperties.Add("MessageType", typeof(T).Name);
            await _topicClient.SendAsync(message);
        }
    }
}
