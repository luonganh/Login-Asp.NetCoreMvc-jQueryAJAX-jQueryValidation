namespace Asp.NetCore.Infrastructure.Identity.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? FullName { get; set; }

        public string? Avatar { get; set; }

        public string? Address { get; set; }

        public Gender? Gender { get; set; }

        public AppUserStatus Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public string? DeletedBy { get; set; }
    }
}
