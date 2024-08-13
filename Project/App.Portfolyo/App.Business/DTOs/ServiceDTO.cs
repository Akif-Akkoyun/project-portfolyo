using PortfolyoApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.DTOs
{
    public class ServiceDTO 
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public DateTime CreatedAt { get; set; }

    }
}
