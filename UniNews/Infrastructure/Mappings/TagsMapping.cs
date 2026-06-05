using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using Uninews.Domain.Entities.Tags;
using Uninews.Domain.Entities.Users;
using Uninews.Domain.Entities.UnitNews;
using Uninews.Domain.Entities.Courses;
using Uninews.Domain.Entities.Ocurrences;

namespace Uninews.Infrastructure.Mappings;

public class TagMapping : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).ValueGeneratedNever();

        builder.OwnsOne(t => t.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.OwnsOne(t => t.Description, description =>
        {
            description.Property(d => d.Value)
                .HasColumnName("Description")
                .HasMaxLength(250)
                .IsRequired();
        });

        // Relacionamento com Usuários (UserTags)
        builder.HasMany(t => t.Users)
            .WithMany(u => u.Tags)
            .UsingEntity<Dictionary<string, object>>(
                "UserTags",
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId")
            );

        // Relacionamento com Notícias (NewsTag) - SEGURO!
        builder.HasMany(t => t.News)
            .WithMany(n => n.Tags)
            .UsingEntity<Dictionary<string, object>>(
                "NewsTag",
                j => j.HasOne<News>().WithMany().HasForeignKey("NewsId"),
                j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId")
            );

        // Relacionamento com Cursos (CourseTags)
        builder.HasMany(t => t.Courses)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "CourseTags",
                j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.NoAction)
            );

        // Relacionamento com Ocorrências (OcurrenceTags) - CORRIGIDO para espelhar perfeitamente o OcurrenceMapping
        builder.HasMany(t => t.Ocurrences)
            .WithMany(o => o.Tags)
            .UsingEntity<Dictionary<string, object>>(
                "OcurrenceTags",
                j => j.HasOne<Ocurrence>().WithMany().HasForeignKey("OcurrenceId").OnDelete(DeleteBehavior.NoAction),
                j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.Cascade)
            );
    }
}