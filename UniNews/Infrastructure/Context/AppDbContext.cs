using Uninews.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Uninews.Domain.Entities.Users;
using System.Reflection;
using Uninews.Domain.Entities.Courses;
using Uninews.Domain.Entities.Ocurrences;
using Uninews.Domain.Entities.Tags;
using Uninews.Domain.Entities.UnitNews;

namespace Uninews.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Ocurrence> Ocurrences => Set<Ocurrence>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<News> News => Set<News>();


    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>()
            .HaveMaxLength(150);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
    {
        foreach (var navigation in entityType.GetNavigations())
        {
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
        modelBuilder.Ignore<Notifications>();
        modelBuilder.Ignore<Notifiable>();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}

