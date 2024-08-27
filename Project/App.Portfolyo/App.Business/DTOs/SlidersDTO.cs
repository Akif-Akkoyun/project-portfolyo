using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.DTOs
{
    public class SlidersDTO
    {
        public long Id { get; set; }
        public string ImgUrl1 { get; set; } = default!;
        public string ImgUrl2 { get; set; } = default!;
    }
}
