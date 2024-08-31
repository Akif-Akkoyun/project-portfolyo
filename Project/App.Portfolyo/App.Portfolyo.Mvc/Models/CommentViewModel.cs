namespace PortfolyoApp.Mvc.Models
{
    public class CommentViewModel
    {
        public long Id { get; set; }
        public long BlogPostId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
