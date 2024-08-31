using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.DTOs
{
    public class CommentDTO
    {
        public long Id { get; set; }
        public long BlogPostId { get; set; }
        public long UserId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string UserName { get; set; } = default!;
    }
}
