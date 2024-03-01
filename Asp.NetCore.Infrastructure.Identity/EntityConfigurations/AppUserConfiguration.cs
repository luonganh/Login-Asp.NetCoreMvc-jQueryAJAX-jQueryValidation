namespace Asp.NetCore.Infrastructure.Identity.EntityConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.FullName).HasMaxLength(254);
            builder.Property(x => x.Address).HasMaxLength(254);
            builder.Property(x => x.Avatar).HasMaxLength(1000);
            builder.Property(x => x.CreatedBy).HasMaxLength(36);
            builder.Property(x => x.UpdatedBy).HasMaxLength(36);
            builder.Property(x => x.DeletedBy).HasMaxLength(36);
        }
    }
}