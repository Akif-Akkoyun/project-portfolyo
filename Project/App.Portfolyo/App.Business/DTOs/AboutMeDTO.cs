using PortfolyoApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.DTOs
{
    public class AboutMeDTO
    {
        public long Id { get; set; }
        public string Introduction { get; set; } = default!;
        public string ImageUrl1 { get; set; } = default!;
        public string CvUrl { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Year { get; set; }
        public int Day { get; set; }
        public string Month { get; set; } = default!;
        public string Address { get; set; } = default!;
        public int ZipCode { get; set; }
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;
    }
}
