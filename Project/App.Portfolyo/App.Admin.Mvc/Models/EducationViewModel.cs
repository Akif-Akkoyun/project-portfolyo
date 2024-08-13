using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PortfolyoApp.Admin.Mvc.Models
{
    public class EducationViewModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Degree { get; set; } = string.Empty!;
        [Required]
        public string School { get; set; } = string.Empty!;
        [Required]
        public int StartDate { get; set; } 
        [Required]
        public int EndDate { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty!;
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
