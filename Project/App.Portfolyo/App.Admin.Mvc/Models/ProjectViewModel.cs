using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolyoApp.Admin.Mvc.Models
{
    public class ProjectViewModel
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty!;
        [Required]
        public string Description { get; set; } = string.Empty!;
        [Required]  
        public IFormFile ImageFile { get; set; } = null!;
        public string ImageUrl { get; set; } = string.Empty!;
        [Required]
        public string Url { get; set; } = string.Empty!;
        [Required]
        public string GithubUrl { get; set; } = string.Empty!;
        [Required]
        public string Tags { get; set; } = string.Empty!;
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
