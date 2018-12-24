using System.Net;
using HireRight.BusinessLogic.Abstract;

namespace HireRight.BusinessLogic.Concrete {
    public class SmtpSettings : ISmtpSettings
    {
        public NetworkCredential SmtpCredentials { get; }
        public int Port { get; }
        public string Host { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public SmtpSettings(NetworkCredential smtpCredentials, int port, string host)
        {
            SmtpCredentials = smtpCredentials;
            Port = port;
            Host = host;
        }
    }
}