namespace E_auction.Business.Exception
{
    /// <summary>
    /// Exception class for buyer API
    /// </summary>
    public class BuyerException : System.Exception
    {
        /// <summary>
        /// Custom exception method to handle the buyer exceptions
        /// </summary>
        /// <param name="message">Specifies to gets the exception message</param>
        public BuyerException(string message) : base(message)
        {
            
        }
    }
}
