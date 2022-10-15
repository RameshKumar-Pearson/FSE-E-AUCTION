namespace E_auction.Business.Models
{
    /// <summary>
    /// Model used to send the gets the email configuration
    /// </summary>
    public class EmailConfiguration
    {
        /// <summary>
        /// Gets (or) sets the from address
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets (or) sets the smtp server 
        /// </summary>
        public string SmtpServer { get; set; }

        /// <summary>
        /// Gets (or) sets the port number
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets (or) sets the smtp user name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets (or) sets the password for smtp
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets (or) sets the subject of the email
        /// </summary>
        public string Subject { get; set; }
    }
}