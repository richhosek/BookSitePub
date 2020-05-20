using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSite.Models
{
    public class SendGridMailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var msg = new SendGridMessage();
            //var recipients = new List<EmailAddress> { new EmailAddress(email) };
            msg.AddTo(email);
            msg.SetSubject(subject);
            msg.AddContent(MimeType.Html, htmlMessage);
            var client = new SendGridClient("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");
            msg.SetFrom(new EmailAddress("richhosek@zana.ws"));
            var response = await client.SendEmailAsync(msg);
        }
    }
}
