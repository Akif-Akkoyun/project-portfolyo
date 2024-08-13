namespace PortfolyoApp.Mvc.Models
{
    public class EducationViewModel
    {
        public long Id { get; set; }
        public string Degree { get; set; } = string.Empty!;
        public string School { get; set; } = string.Empty!;
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public string Description { get; set; } = string.Empty!;
        public DateTime CreatedAt { get; set; }
    }
}
