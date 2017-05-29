﻿using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Configuration;
using HireRight.BusinessLogic.Abstract;

namespace HireRight.BusinessLogic.Concrete
{
    public class EmailSender : IEmailSender
    {
        private static readonly NetworkCredential _smtpCredentials;
        private static readonly string DianaEmail;
        private static readonly string JanetEmail;
        private static readonly string MailServiceEmailAddress;
        private readonly string _emailTemplatePath;

        static EmailSender()
        {
            MailServiceEmailAddress = WebConfigurationManager.AppSettings["MailServiceEmailAddress"];
            _smtpCredentials = new NetworkCredential(MailServiceEmailAddress, WebConfigurationManager.AppSettings["MailAccountPassword"]);
            DianaEmail = WebConfigurationManager.AppSettings["DianaEmail"];
            JanetEmail = WebConfigurationManager.AppSettings["JanetEmail"];
        }

        public EmailSender(string emailTemplatePath)
        {
            _emailTemplatePath = emailTemplatePath;
        }

        public void EmailConsultants(string message, string subject, string replyTo = null)
        {
            SendFormattedEmail(DianaEmail, "Diana", message, subject, replyTo);
            SendFormattedEmail(JanetEmail, "Janet", message, subject, replyTo);
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
                { IsBodyHtml = true };
                if (replyTo != null)
                    mailMessage.ReplyToList.Add(replyTo);

                SetEmailPickupDirectory(emailClient);
                emailClient.Credentials = _smtpCredentials;
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

            string messageBody = string.Format(body, greeting, message);
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