using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace  ECommerceCMS.Helpers
{
    public static class MailSender
    {
        public static bool SendMail(string to, string subject, string content)
        {
            try
            {
                string fromEmail = "antasya.development@gmail.com"; //TODO this should be read from config file
                string fromPassword = "modyoAntasya ";
                var mailMessage = CreateHtmlMailMessage(fromEmail, to, subject, content);
                var smtpClient = CreateSmtpClient(fromEmail, fromPassword);
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.Write("Exception occured while sending mail: ", ex);
                return false;
            }
        }

        private static MailMessage CreateHtmlMailMessage(string fromEmail, string to, string subject, string content)
        {
            MailMessage mailMessage = new MailMessage(fromEmail, to, subject, content);
            mailMessage.IsBodyHtml = true;
            mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(content, null, MediaTypeNames.Text.Html));
            return mailMessage;

        }

        private static SmtpClient CreateSmtpClient(string fromEmail, string fromPassword)
        {
            return new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, fromPassword) //todo this value should be read from config file
            };
        }

    }
}
