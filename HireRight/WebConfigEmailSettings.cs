using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Configuration;
using HireRight.BusinessLogic.Abstract;
using HireRight.BusinessLogic.Concrete;

namespace HireRight
{
    public class ServerPathReader {
        public static string MapServerPath(string relativePath)
        {
            var serverPath = HttpContext.Current.Server.MapPath("~");
            var path = Path.Combine(serverPath, relativePath);
            return path;
        }
    }

    public class WebConfigEmailSettings : IEmailSettings
    {
        private readonly List<(string email, string name)> _consultants;

        public string SenderEmailAddress { get; }

        public ISmtpSettings SmtpSettings { get; }

        public IReadOnlyList<(string email, string name)> ConsultantEmailAddresses => _consultants.AsReadOnly();

        public string EmailTemplatePath { get; }

        public WebConfigEmailSettings()
        {
            SenderEmailAddress = WebConfigurationManager.AppSettings["MailServiceEmailAddress"];
            SmtpSettings = new SmtpSettings(new NetworkCredential(SenderEmailAddress, WebConfigurationManager.AppSettings["MailAccountPassword"]), 25, WebConfigurationManager.AppSettings["SmtpHost"]);
            EmailTemplatePath = ServerPathReader.MapServerPath(WebConfigurationManager.AppSettings["EmailTemplatePath"]);

            _consultants = new List<(string email, string name)>();
            _consultants.Add((WebConfigurationManager.AppSettings["DianaEmail"], "Diana"));
            _consultants.Add((WebConfigurationManager.AppSettings["JanetEmail"], "Janet"));
        }
    }
}