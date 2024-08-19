namespace PortfolyoApp.Admin.Mvc.Models
{
    public class AppUserViewModel
    {

        public long Id { get; set; }
        public string UserName { get; set; } = default!;
        public string UserSurName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
