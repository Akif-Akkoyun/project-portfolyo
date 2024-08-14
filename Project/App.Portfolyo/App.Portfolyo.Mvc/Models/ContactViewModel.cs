using System.ComponentModel.DataAnnotations;

namespace PortfolyoApp.Mvc.Models
{
    public class ContactViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "İsim gerekli.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "E-posta gerekli.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Konu gerekli.")]
        public string Subject { get; set; } = null!;

        [Required(ErrorMessage = "Mesaj gerekli.")]
        public string Message { get; set; } = null!;
        public DateTime SentDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
