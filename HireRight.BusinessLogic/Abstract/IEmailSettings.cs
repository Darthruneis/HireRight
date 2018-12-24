using System.Collections.Generic;

namespace HireRight.BusinessLogic.Abstract
{
    public interface IEmailSettings
    {
        string SenderEmailAddress { get; }
        ISmtpSettings SmtpSettings { get; }
        IReadOnlyList<(string  email, string name)> ConsultantEmailAddresses { get; }
        string EmailTemplatePath { get; }
    }
}