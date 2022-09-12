using DataAccess.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string recipient, string body, string subject)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse("douglas27@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(recipient));
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smpt = new SmtpClient();
            smpt.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smpt.Authenticate("douglas27@ethereal.email", "zwtXUM99UbWYrY8AUK");
            smpt.Send(email);
            smpt.Disconnect(true);
        }
    }
}
