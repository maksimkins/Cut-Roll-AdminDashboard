namespace Cut_Roll_AdminDashboard.Infrastructure.Common.Data;

using Cut_Roll_AdminDashboard.Core.Roles.Configurations;
using Cut_Roll_AdminDashboard.Core.Roles.Models;
using Cut_Roll_AdminDashboard.Core.Users.Configurations;
using Cut_Roll_AdminDashboard.Core.Users.Models;
using Microsoft.EntityFrameworkCore;

public class CutRollAdminDashboardDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public CutRollAdminDashboardDbContext(DbContextOptions options) : base(options)
    { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
}