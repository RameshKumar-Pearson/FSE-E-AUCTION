using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace E_auction.Business.Models
{
    /// <summary>
    /// Model used to define the message of the email
    /// </summary>
    public class EmailMessage
    {
        /// <summary>
        /// Gets (or) sets To email address
        /// </summary>
        public List<MailboxAddress> To { get; set; }

        /// <summary>
        /// Gets (or) sets  email subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets (or) sets email content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Method used to prepare the mail parameters
        /// </summary>
        /// <param name="to">Specifies to gets the To email address</param>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        public EmailMessage(IEnumerable<EmailAddress> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x.DisplayName,x.Address)));
            Subject = subject;
            Content = content;
        }
    }
}