using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.DTOs.Auth
{
    public class UserGetResult
    {
        public long Id { get; set; }
        public string UserName { get; set; } = default!;
        public string UserSurName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Role { get; set; } = null!;
    }
}
