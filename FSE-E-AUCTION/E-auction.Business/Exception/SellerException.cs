using System;

namespace E_auction.Business.Exception
{
    /// <summary>
    ///     Exception class for seller API
    /// </summary>
    [Serializable]
    public class SellerException : System.Exception
    {
        /// <summary>
        ///     Method used to throw the seller exception
        /// </summary>
        /// <param name="message">Specifies to gets the exception  message</param>
        public SellerException(string message) : base(message)
        {
        }

        /// <summary>
        ///     Method used to throw the seller exception for bid endDate
        /// </summary>
        /// <param name="bidEndDate">Specifies to gets the bid endDate</param>
        public SellerException(DateTime bidEndDate)
            : base(string.Format("Bid end date should be future date: {0}", bidEndDate))
        {
        }
    }
}