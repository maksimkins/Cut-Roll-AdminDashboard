using Cut_Roll_AdminDashboard.Core.Roles.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cut_Roll_AdminDashboard.Core.Roles.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {

        builder.HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .ValueGeneratedNever(); 

        builder.Property(u => u.Name)
            .IsRequired();

        builder.HasIndex(u => u.Name)
            .IsUnique();

        builder.HasMany(r => r.Users)
            .WithOne(u => u.Role)          
            .HasForeignKey(u => u.RoleId)  
            .OnDelete(DeleteBehavior.Cascade);
    }
}