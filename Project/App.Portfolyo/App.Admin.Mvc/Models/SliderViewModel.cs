namespace PortfolyoApp.Admin.Mvc.Models
{
    public class SliderViewModel
    {
        public long Id { get; set; }
        public string ImgUrl1 { get; set; } = default!;
        public IFormFile ImgFile1 { get; set; } = null!;
        public string ImgUrl2 { get; set; } = default!;
        public IFormFile ImgFile2 { get; set; } = null!;
    }
}
