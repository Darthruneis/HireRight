using System.Net;

namespace HireRight.BusinessLogic.Abstract {
    public interface ISmtpSettings
    {
        NetworkCredential SmtpCredentials { get; }
        int Port { get; }
        string Host { get; }
    }
}