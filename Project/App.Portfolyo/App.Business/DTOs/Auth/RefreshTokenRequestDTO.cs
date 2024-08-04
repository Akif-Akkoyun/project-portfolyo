using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.DTOs.Auth
{
    public class RefreshTokenRequestDTO
    {
        public string Token { get; set; } = default!;
    }
}
