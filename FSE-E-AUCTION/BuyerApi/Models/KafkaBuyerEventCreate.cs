using BuyerApi.RequestModels;
using BuyerApi.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerApi.Models
{
    /// <summary>
    /// Class having the properties which holds the value of kafka topic with buyer details....
    /// </summary>
    public class KafkaBuyerEventCreate
    {
        /// <summary>
        /// Gets(or) Sets the topic of kafka
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// Gets (or) Sets the Topic Message
        /// </summary>
        public MongoBuyerResponse TopicMessage { get; set; }
    }
}
