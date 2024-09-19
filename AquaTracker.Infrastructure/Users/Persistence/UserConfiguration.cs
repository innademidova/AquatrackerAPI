using AquaTracker.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaTracker.Infrastructure.Users.Persistence;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(u => u.Email)
            .IsUnique();
        
        builder.HasMany(u => u.WaterEntries)
            .WithOne(w => w.User)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}