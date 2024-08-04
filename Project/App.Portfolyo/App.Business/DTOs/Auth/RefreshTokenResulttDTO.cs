using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.DTOs.Auth
{
    public class RefreshTokenResulttDTO
    {
        public string Token { get; set; } = default!;
    }
    public class RefreshTokenResulttDTOValidator : AbstractValidator<RefreshTokenResulttDTO>
    {
        public RefreshTokenResulttDTOValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
        }
    }
}
