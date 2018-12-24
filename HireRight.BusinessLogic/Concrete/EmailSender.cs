using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using HireRight.BusinessLogic.Abstract;

namespace HireRight.BusinessLogic.Concrete
{
    public class EmailSender : IEmailSender
    {
        private readonly IEmailSettings _emailSettings;

        public EmailSender(IEmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public void EmailConsultants(string message, string subject, string replyTo = null)
        {
#if DEBUG
    return;
#endif
#pragma warning disable 162
            // ReSharper disable HeuristicUnreachableCode
            foreach (var email in _emailSettings.ConsultantEmailAddresses)
            {
                SendFormattedEmail(email.email, email.name, message, subject, replyTo);
                SendFormattedEmail(email.email, email.name, message, subject, replyTo);
            }
            
            SendFormattedEmail("info@HireRightTesting.com", "All", message, subject, replyTo);
            // ReSharper restore HeuristicUnreachableCode
#pragma warning restore 162
        }

        public void SendEmail(string recipient, string body, string subject, string replyTo = null)
        {
            using (SmtpClient emailClient = new SmtpClient())
            {
                MailMessage mailMessage = new MailMessage(
                    _emailSettings.SenderEmailAddress,
                    recipient,
                    subject,
                    //Replace normal line breaks with HTML break statements
                    body.Replace("\r\n", "<br/>"))
                { IsBodyHtml = true, CC = { "" }};
                if (replyTo != null)
                    mailMessage.ReplyToList.Add(replyTo);

                SetEmailPickupDirectory(emailClient);
#if !DEBUG
                emailClient.Host = _emailSettings.SmtpSettings.Host;
                emailClient.Port = _emailSettings.SmtpSettings.Port;
                emailClient.Credentials = _emailSettings.SmtpSettings.SmtpCredentials;
#endif
                emailClient.Send(mailMessage);
            }
        }

        public void SendFormattedEmail(string email, string greeting, string message, string subject, string replyTo = null)
        {
            string body;
            using (StreamReader reader = new StreamReader(_emailSettings.EmailTemplatePath))
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