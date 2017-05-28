namespace HireRight.BusinessLogic.Abstract
{
    public interface IEmailSender
    {
        void EmailConsultants(string message, string subject, string replyTo = null);

        void SendEmail(string recipient, string body, string subject, string replyTo = null);

        void SendFormattedEmail(string email, string greeting, string message, string subject, string replyTo = null);
    }
}