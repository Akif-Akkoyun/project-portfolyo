using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolyoApp.Mvc.Models
{
    public class ServiceViewModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = default!;
        public DateTime CreatedAt { get; set; }

    }
}
