using Uninews.Domain.Entities;
using Uninews.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Uninews.Domain.Entities.Users;

namespace Uninews.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();


    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>()
            .HaveMaxLength(150);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Notifications>();
        modelBuilder.Ignore<Notifiable>();
    }
}

