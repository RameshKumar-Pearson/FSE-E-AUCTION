using System.Threading.Tasks;

namespace E_auction.Business.MessagePublishers
{
    /// <summary>
    /// Interface class for publishing message to service bus trigger
    /// </summary>
    public interface IMessagePublisher
    {
        /// <summary>
        /// Model used to publish the message to service bus
        /// </summary>
        /// <param name="request">Specifies to gets the request for publish the message to the topic</param>
        /// <returns>Awaitable task with no data</returns>
        Task PublisherAsync<T>(T request);
    }
}
