using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerApi.MessagePublishers
{
    public interface IMessagePublisher
    {
        Task PublisherAsync<T>(T request);
    }
}
