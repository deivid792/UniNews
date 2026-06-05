using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using Uninews.Domain.Entities.UnitNews;

namespace Uninews.Infrastructure.Mappings;

public class NewsMapping : IEntityTypeConfiguration<News>
{
    public void Configure(EntityTypeBuilder<News> builder)
    {
        builder.ToTable("News");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id).ValueGeneratedNever();

        builder.Property(n => n.Date)
            .IsRequired();

        builder.Property(n => n.Time)
            .IsRequired();

        builder.Property(n => n.Link)
            .HasColumnName("Link")
            .HasMaxLength(500)
            .IsRequired(false);

        builder.OwnsOne(n => n.Title, title =>
        {
            title.Property(t => t.Value)
                .HasColumnName("Title")
                .HasMaxLength(150)
                .IsRequired();
        });

        builder.OwnsOne(n => n.Description, description =>
        {
            description.Property(d => d.Value)
                .HasColumnName("Description")
                .HasMaxLength(2000)
                .IsRequired();
        });

        builder.HasOne(n => n.User)
            .WithMany(u => u.News)
            .HasForeignKey("UserId")
            .IsRequired();

        builder.HasMany(n => n.Tags)
            .WithMany(t => t.News)
            .UsingEntity<Dictionary<string, object>>(
                "NewsTag",
                j => j.HasOne<Uninews.Domain.Entities.Tags.Tag>().WithMany().HasForeignKey("TagId"),
                j => j.HasOne<News>().WithMany().HasForeignKey("NewsId")
            );
    }
}