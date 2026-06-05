using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using Uninews.Domain.Entities.Users;
using Uninews.Domain.Entities.Tags;

namespace Uninews.Infrastructure.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.OwnsOne(u => u.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();
        });
        
        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .HasMaxLength(254)
                .IsRequired();
        });

        builder.OwnsOne(u => u.Password, password =>
        {
            password.Property(p => p.Value)
                .HasColumnName("Password")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(u => u.CPF, cpf =>
        {
            cpf.Property(c => c.Value)
                .HasColumnName("Cpf")
                .HasMaxLength(14)
                .IsRequired();
        });

        // Corrigido para evitar o "CourseId1": apontamos explicitamente que Course não tem uma lista reversa de Users
        builder.HasOne(u => u.Course)
            .WithMany() 
            .HasForeignKey("CourseId")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRoles",
                j => j.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
            );

        builder.HasMany(u => u.Tags)
            .WithMany(t => t.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserTags",
                j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId")
            );

        // Relacionamento com News (Ok, mantido)
        builder.HasMany(u => u.News)
            .WithOne(n => n.User)
            .HasForeignKey("UserId");
    }
}