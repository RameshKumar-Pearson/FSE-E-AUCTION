using E_auction.Business.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace E_auction.Business.Services.EmailService
{
    /// <summary>
    /// Class used to send email to the seller when the product deletion happen.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly ILogger<EmailSender> _logger;

        #region public methods

        /// <summary>
        /// Constructor for <see cref="EmailSender"/>
        /// </summary>
        /// <param name="emailConfig">Specifies to gets the email configuration</param>
        /// <param name="logger">Specifies to gets the <see cref="ILogger"/></param>
        public EmailSender(IOptions<EmailConfiguration> emailConfig, ILogger<EmailSender> logger)
        {
            _emailConfig = emailConfig.Value;
            _logger = logger;
        }

        ///<inheritdoc cref="IEmailSender"/>
        public void SendEmail(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        #endregion

        #region private methods

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email",_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch(System.Exception ex)
                {
                    //Due to the firewall issue in the VM, email will not sent. so as of now i ignore the exception to continue with the process..
                   _logger.LogError(ex,"exception occurred while sending the email");
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        #endregion
    }
}