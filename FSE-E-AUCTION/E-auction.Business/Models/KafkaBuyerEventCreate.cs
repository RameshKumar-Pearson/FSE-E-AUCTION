using E_auction.Business.RequestModels;

namespace E_auction.Business.Models
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
        public SaveBuyerRequestModel TopicMessage { get; set; }
    }
}