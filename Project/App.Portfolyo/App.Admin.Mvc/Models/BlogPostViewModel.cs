using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolyoApp.Admin.Mvc.Models
{
    public class BlogPostViewModel
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; } = default!;
        [Required]
        public string Content { get; set; } = default!;
        [Required]
        public IFormFile ImageFile { get; set; } = null!;
        [MaxLength(255)]
        public string ImageUrl { get; set; } = default!;
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
