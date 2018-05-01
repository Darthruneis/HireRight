using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Configuration;
using HireRight.BusinessLogic.Abstract;

namespace HireRight.BusinessLogic.Concrete
{
    public class EmailSender : IEmailSender
    {
        private readonly string _emailTemplatePath;
        private readonly string DianaEmail;
        private readonly string JanetEmail;
        private readonly string MailServiceEmailAddress;
#pragma warning disable 414
        // ReSharper disable NotAccessedField.Local
        private readonly NetworkCredential _smtpCredentials;
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        // ReSharper restore NotAccessedField.Local
#pragma warning restore 414

        public EmailSender()
        {
            MailServiceEmailAddress = WebConfigurationManager.AppSettings["MailServiceEmailAddress"];
            _smtpCredentials = new NetworkCredential(MailServiceEmailAddress, WebConfigurationManager.AppSettings["MailAccountPassword"]);
            DianaEmail = WebConfigurationManager.AppSettings["DianaEmail"];
            JanetEmail = WebConfigurationManager.AppSettings["JanetEmail"];
            _smtpPort = 25;
            _smtpHost = WebConfigurationManager.AppSettings["SmtpHost"];
            _emailTemplatePath = WebConfigurationManager.AppSettings["EmailTemplatePath"];
        }

        public void EmailConsultants(string message, string subject, string replyTo = null)
        {
#if DEBUG
    return;
#endif
#pragma warning disable 162
            // ReSharper disable HeuristicUnreachableCode
            SendFormattedEmail(DianaEmail, "Diana", message, subject, replyTo);
            SendFormattedEmail(JanetEmail, "Janet", message, subject, replyTo);
            SendFormattedEmail("info@HireRightTesting.com", "All", message, subject, replyTo);
            // ReSharper restore HeuristicUnreachableCode
#pragma warning restore 162
        }

        public void SendEmail(string recipient, string body, string subject, string replyTo = null)
        {
            using (SmtpClient emailClient = new SmtpClient())
            {
                MailMessage mailMessage = new MailMessage(
                    MailServiceEmailAddress,
                    recipient,
                    subject,
                    //Replace normal line breaks with HTML break statements
                    body.Replace("\r\n", "<br/>"))
                { IsBodyHtml = true, CC = { "" }};
                if (replyTo != null)
                    mailMessage.ReplyToList.Add(replyTo);

                SetEmailPickupDirectory(emailClient);
#if !DEBUG
                emailClient.Host = _smtpHost;
                emailClient.Port = _smtpPort;
                emailClient.Credentials = _smtpCredentials;
#endif
                emailClient.Send(mailMessage);
            }
        }

        public void SendFormattedEmail(string email, string greeting, string message, string subject, string replyTo = null)
        {
            string body;
            using (StreamReader reader = new StreamReader(_emailTemplatePath))
            {
                body = reader.ReadToEnd();
            }

            string messageBody = string.Format(body, greeting, message, ConfigurationManager.AppSettings["HireRightBaseUrl"]);
            SendEmail(email, messageBody, subject, replyTo);
        }

        [Conditional("DEBUG")]
        private void SetEmailPickupDirectory(SmtpClient emailClient)
        {
            emailClient.PickupDirectoryLocation = @"C:\Users\Chris\Desktop\HireRight\HireRight Test Emails";
            emailClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
        }
    }
}