using BuyerApi.Contracts.CommandHandlers;
using BuyerApi.Models;
using BuyerApi.RequestModels;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerApi.KafkaConsumerService
{
    /// <summary>
    /// Class used to consume the kafka buyer event...
    /// </summary>
    public class BuyerEventConsumer : IConsumer<KafkaBuyerEventCreate>
    {
        private readonly ISaveBuyerCommandHandler _saveBuyerCommandHandler;

        #region Public Methods

        /// <summary>
        /// constructor for <see cref="BuyerEventConsumer"/>
        /// </summary>
        /// <param name="saveBuyerCommandHandler">Specifies to gets <see cref="ISaveBuyerCommandHandler"/></param>
        public BuyerEventConsumer(ISaveBuyerCommandHandler saveBuyerCommandHandler)
        {
            _saveBuyerCommandHandler = saveBuyerCommandHandler;
        }

        /// <inheritdoc/>
        public Task Consume(ConsumeContext<KafkaBuyerEventCreate> context)
        {
            SaveBuyerRequestModel message = context.Message.TopicMessage;
            if (message != null)
            {
                _saveBuyerCommandHandler.AddBid(message);
            }
            return Task.CompletedTask;
        }

        #endregion
    }
}
