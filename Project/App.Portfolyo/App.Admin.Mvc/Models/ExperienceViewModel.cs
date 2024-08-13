
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolyoApp.Admin.Mvc.Models
{
    public class ExperienceViewModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; } = default!;
        [Required]
        public string Company { get; set; } = default!;
        [Required]
        public string StartDate { get; set; } = default!;
        [Required]
        public string EndtDate { get; set; } = default!;
        [Required]
        public string Description { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
