using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_auction.Business.MessagePublishers
{
    /// <summary>
    /// Method used to publish the message to message to azure service bus for deleting the product
    /// </summary>
    public class MessagePublisher : IMessagePublisher
    {
        private readonly ITopicClient _topicClient;

        /// <summary>
        /// Constructor for <see cref="MessagePublisher"/>
        /// </summary>
        /// <param name="topicClient"></param>
        public MessagePublisher(ITopicClient topicClient)
        {
            _topicClient = topicClient;
        }

        ///<inheritdoc/>
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
