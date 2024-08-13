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
        public string StartDate { get; set; } = default!;
        public string EndtDate { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
