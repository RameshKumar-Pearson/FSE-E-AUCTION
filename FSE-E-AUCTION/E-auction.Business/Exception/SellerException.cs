using System;

namespace E_auction.Business.Exception
{
    /// <summary>
    ///  Exception class for seller API
    /// </summary>
    [Serializable]
    public class SellerException : System.Exception
    {
        public SellerException(string message) : base(message)
        {

        }

        public SellerException(DateTime bidEndDate)
            : base(string.Format("Bid end date should be future date: {0}", bidEndDate))
        {
        }

    }
}
