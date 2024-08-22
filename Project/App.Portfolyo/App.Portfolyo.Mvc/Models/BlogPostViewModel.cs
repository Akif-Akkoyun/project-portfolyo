using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolyoApp.Mvc.Models
{
    public class BlogPostViewModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public DateTime PublishDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
