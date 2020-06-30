using Microsoft.AspNet.Identity;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using SendGrid;
using System.Diagnostics;

namespace NewsSite.App_Start
{
    public class IdentityConfig
    {
        public class EmailService : IIdentityMessageService
        {
        
            public async Task SendAsync(IdentityMessage message)
            {
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("isabelle.tounsi@hotmail.com", "News site - goto news");
                var to = new EmailAddress(message.Destination, "Example User");
                var subject = message.Subject;
                var plainTextContent = message.Body;
                var htmlContent = message.Body;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                if (client != null)
                {
                    var response = await client.SendEmailAsync(msg);
                }
                else
                {
                    Trace.TraceError("Failed to create Web transport.");
                    await Task.FromResult(0);
                }



            }
        }
    }
}