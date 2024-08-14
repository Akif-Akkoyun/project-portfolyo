using PortfolyoApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.DTOs
{
    public class ExperienceDTO 
    {
        public long Id { get; set; }
        public string Title { get; set; } = default!;
        public string Company { get; set; } = default!;
        public int StartYear { get; set; }
        public string StartMonth { get; set; } = default!;
        public string EndMonth { get; set; } = default!;
        public int EndtYear { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
