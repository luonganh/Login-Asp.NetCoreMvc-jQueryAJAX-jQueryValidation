namespace Asp.NetCore.Infrastructure.Identity.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public string? DeletedBy { get; set; }
    }
}
