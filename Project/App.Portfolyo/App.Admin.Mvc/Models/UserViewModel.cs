using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolyoApp.Admin.Mvc.Models
{
    public class UserViewModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required, MaxLength(50)]
        public string UserName { get; set; } = default!;
        [Required, MaxLength(50)]
        public string UserSurName { get; set; } = default!;
        [Required,EmailAddress]
        public string Email { get; set; } = default!;
        [Required, MaxLength(255)]
        public string Role { get; set; } = default!;
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
