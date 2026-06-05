using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using Uninews.Domain.Entities.Ocurrences;
using Uninews.Domain.Entities.Users;
using Uninews.Domain.Entities.Tags;

namespace Uninews.Infrastructure.Mappings;

public class OcurrenceMapping : IEntityTypeConfiguration<Ocurrence>
{
    public void Configure(EntityTypeBuilder<Ocurrence> builder)
    {
        builder.ToTable("Ocurrences");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id).ValueGeneratedNever();

        builder.Property(o => o.Date)
            .IsRequired();

        builder.Property(o => o.Time)
            .IsRequired();

        builder.Property(o => o.Link)
            .HasColumnName("Link")
            .HasMaxLength(500) // Ajuste o tamanho conforme necessário
            .IsRequired(false);


        builder.OwnsOne(o => o.Title, title =>
        {
            title.Property(t => t.Value)
                .HasColumnName("Title")
                .HasMaxLength(150)
                .IsRequired();
        });

        builder.OwnsOne(o => o.Category, category =>
        {
            category.Property(c => c.Value)
                .HasColumnName("Category")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(o => o.Description, description =>
        {
            description.Property(d => d.Value)
                .HasColumnName("Description")
                .HasMaxLength(2000)
                .IsRequired();
        });

        builder.OwnsOne(o => o.Minister, minister =>
        {
            minister.Property(m => m.Value)
                .HasColumnName("Minister")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.OwnsOne(o => o.Location, location =>
        {
            location.Property(l => l.Value)
                .HasColumnName("Location")
                .HasMaxLength(250)
                .IsRequired();
        });

        builder.HasOne(o => o.User)
            .WithMany(u => u.Ocurrences)
            .HasForeignKey("UserId")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(o => o.Participants)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "OcurrenceUser",
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Ocurrence>().WithMany().HasForeignKey("OcurrenceId").OnDelete(DeleteBehavior.NoAction)
            );

        builder.HasMany(o => o.Tags)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "OcurrenceTags",
                j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Ocurrence>().WithMany().HasForeignKey("OcurrenceId").OnDelete(DeleteBehavior.NoAction)
            );
    }
}