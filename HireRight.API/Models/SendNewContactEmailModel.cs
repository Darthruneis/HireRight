using System;

namespace HireRight.API.Models
{
    public class SendNewContactEmailModel
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}