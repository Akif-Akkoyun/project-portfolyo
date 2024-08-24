using System.ComponentModel.DataAnnotations;

namespace PortfolyoApp.Mvc.Models
{
    public class LoginViewModel
    {
        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; } = null!;
        [Required, DataType(DataType.Password), MinLength(4)]
        public string Password { get; set; } = null!;
    }
}
