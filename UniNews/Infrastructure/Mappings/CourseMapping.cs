using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uninews.Domain.Entities.Courses;

namespace Uninews.Infrastructure.Mappings;

public class CourseMapping : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.OwnsOne(c => c.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.HasOne(c => c.Tag)
            .WithMany()
            .HasForeignKey("TagId")
            .IsRequired();

        builder.HasMany(c => c.Users)
            .WithOne(u => u.Course)
            .HasForeignKey("CourseId");
    }
}