using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolyoApp.Business.DTOs
{
    public class ContactDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty!;
        public string Email { get; set; } = string.Empty!;
        public string Message { get; set; } = string.Empty!;
        public string Subject { get; set; } = string.Empty!;
        public DateTime SentDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
