using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NewsSite.Service
{
    public class EmailConfirmationService
    {
        private async Task ConfigSendGridasync(IdentityMessage message)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("isabelle.tounsi@hotmail.com", "News site - goto news");
            var to = new EmailAddress(message.Destination, "Example User");
            var subject = "Confirm your account";
            var plainTextContent = "Reset your password";
            var htmlContent = "<p>Click on the link to confirm your emailq<p><br><a href=http://localhost:60820/Account/ConfirmEmail> Click here</a>";
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