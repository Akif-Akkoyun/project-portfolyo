namespace PortfolyoApp.Data
{
    public abstract class EntityBase
    {
        public long Id { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
    }
}
