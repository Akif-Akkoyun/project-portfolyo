using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.DTOs
{
    public class BlogPostDTO
    {
        public long Id { get; set; }
        public string Title { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public DateTime PublishDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
