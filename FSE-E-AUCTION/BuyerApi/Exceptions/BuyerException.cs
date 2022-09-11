using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerApi.Exceptions
{
    /// <summary>
    /// Exception class for buyer API
    /// </summary>
    public class BuyerException : Exception
    {
        /// <summary>
        /// Custom exception method to handle the buyer exceptions
        /// </summary>
        /// <param name="message">Specifies to gets the exception message</param>
        public BuyerException(string message) { }
    }
}
