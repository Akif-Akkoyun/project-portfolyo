namespace PortfolyoApp.Mvc.Models
{
    public class ExperienceViewModel
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
