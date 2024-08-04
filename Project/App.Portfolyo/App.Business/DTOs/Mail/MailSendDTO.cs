using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.DTOs.Mail
{
    public class MailSendDTO
    {
        public string[] To { get; set; } = null!;
        public string[]? Cc { get; set; }
        public string[]? Bcc { get; set; }
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public bool IsHtml { get; set; }
    }
    public class MailSendDTOValidator : AbstractValidator<MailSendDTO>
    {
        public MailSendDTOValidator()
        {
            RuleFor(x => x.To).NotEmpty().ForEach(x => x.EmailAddress());
            RuleFor(x => x.Cc).ForEach(x => x.EmailAddress());
            RuleFor(x => x.Bcc).ForEach(x => x.EmailAddress());
            RuleFor(x => x.Subject).NotEmpty();
            RuleFor(x => x.Body).NotEmpty();
        }
    }
}
