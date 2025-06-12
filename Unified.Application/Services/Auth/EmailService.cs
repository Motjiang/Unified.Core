using Mailjet.Client.TransactionalEmails;
using Mailjet.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Auth;

namespace Unified.Application.Services.Auth
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendEmailAsync(EmailSendDto emailSend)
        {
            MailjetClient client = new MailjetClient(_config["MailJet:ApiKey"], _config["MailJet:SecretKey"]);

            var email = new TransactionalEmailBuilder()
                 .WithFrom(new SendContact(_config["Email:From"], _config["Email:ApplicationName"]))
                 .WithSubject(emailSend.Subject)
                 .WithHtmlPart(emailSend.Body)
                 .WithTo(new SendContact(emailSend.To))
                 .Build();

            var response = await client.SendTransactionalEmailAsync(email);
            if (response.Messages != null)
            {
                if (response.Messages[0].Status == "success")
                {
                    return true;
                }
            }

            return false;
        }
    }
}
