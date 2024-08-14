using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolyoApp.Admin.Mvc.Models
{
    public class ContactViewModel
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Message { get; set; } = null!;
        [Required]
        public string Subject { get; set; } = null!;
        [Required]
        public DateTime SentDate { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
