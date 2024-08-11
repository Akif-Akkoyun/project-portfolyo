using System.ComponentModel.DataAnnotations;

namespace PortfolyoApp.Admin.Mvc.Models
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = default!;
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = default!;
    }
}
