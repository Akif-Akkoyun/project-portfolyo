namespace PortfolyoApp.Mvc.Models
{
    public class ExperienceViewModel
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
