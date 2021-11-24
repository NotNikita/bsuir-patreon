using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;
        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendAsync(string email, string subject, string text)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("BoostIt", _configuration["email"]));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = text
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 25, false);
                await client.AuthenticateAsync(_configuration["email"], _configuration["emailPass"]);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
    }
}
