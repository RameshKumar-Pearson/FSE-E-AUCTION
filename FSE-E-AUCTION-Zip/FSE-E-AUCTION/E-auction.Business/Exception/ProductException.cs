namespace E_auction.Business.Exception
{
    /// <summary>
    /// Class used to throw the product exception
    /// </summary>
   public class ProductException : System.Exception { 
        /// <summary>
        /// class used to handle product related exception
        /// </summary>
        /// <param name="message"></param>
        public ProductException(string message) : base(message) {}
    }
}
