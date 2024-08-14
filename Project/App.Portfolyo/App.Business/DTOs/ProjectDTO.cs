using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.DTOs
{
    public class ProjectDTO
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty!;
        public string Description { get; set; } = string.Empty!;
        public string ImageUrl { get; set; } = string.Empty!;
        public string Url { get; set; } = string.Empty!;
        public string GithubUrl { get; set; } = string.Empty!;
        public string Tags { get; set; } = string.Empty!;
        public DateTime CreatedAt { get; set; }
    }
}
