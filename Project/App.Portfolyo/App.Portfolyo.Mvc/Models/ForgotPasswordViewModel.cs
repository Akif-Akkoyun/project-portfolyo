using System.ComponentModel.DataAnnotations;

namespace PortfolyoApp.Mvc.Models
{
    public class ForgotPasswordViewModel
    {
        [Required, MaxLength(256), EmailAddress]
        public string Email { get; set; }=null!;
    }
}
