using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Prova.MVC.Services
{
    public class SendEmailService
    {
        string _sender = "";
        string _password = "";
        public SendEmailService(string sender, string password)
        {
            _sender = sender;
            _password = password;
        }

        public void Send(string recipient, string subject, string message)
        {
            using (var client = new SmtpClient("smtp-mail.outlook.com"))
            {
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                var credentials = new System.Net.NetworkCredential(_sender, _password);
                client.EnableSsl = true;
                client.Credentials = credentials;
                var mail = new MailMessage(_sender.Trim(), recipient.Trim())
                {
                    Subject = subject,
                    Body = message
                };

                client.Send(mail);
            }
        }
    }
}