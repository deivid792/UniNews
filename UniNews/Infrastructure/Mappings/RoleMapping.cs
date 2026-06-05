using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uninews.Domain.Entities.Users;

namespace Uninews.Infrastructure.Mappings;

public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id).ValueGeneratedNever();

        builder.OwnsOne(r => r.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.OwnsOne(r => r.Description, description =>
        {
            description.Property(d => d.Value)
                .HasColumnName("Description")
                .HasMaxLength(250)
                .IsRequired();
        });

        builder.HasMany(r => r.Users)
            .WithMany(u => u.Roles)
            .UsingEntity<Dictionary<string, object>>(
                "UserRoles",
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId")
            );
    }
}