using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerApi.Exception
{
    [Serializable]
    public class InvalidBidDateException : System.Exception
    {
        public InvalidBidDateException() { }

        public InvalidBidDateException(DateTime bidEndDate)
       : base(string.Format("Bid end date should be future date: {0}", bidEndDate))
        {

        }
    }
}
