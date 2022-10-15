using E_auction.Business.Models;

namespace E_auction.Business.Services.EmailService
{
    /// <summary>
    /// Interface used to send the email
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Method used to send email
        /// </summary>
        /// <param name="message"></param>
        void SendEmail(EmailMessage message);
    }
}
