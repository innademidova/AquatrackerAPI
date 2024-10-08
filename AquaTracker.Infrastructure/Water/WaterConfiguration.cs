using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaTracker.Infrastructure.Water;

public class WaterConfiguration : IEntityTypeConfiguration<Domain.Water.WaterEntry>
{
    public void Configure(EntityTypeBuilder<Domain.Water.WaterEntry> builder)
    {
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(w => w.Amount)
            .IsRequired();

        builder.Property(w => w.Date)
            .IsRequired();

        builder.Property(w => w.LoggedTime)
            .IsRequired();
        
        builder.HasOne(w => w.User)
            .WithMany(u => u.WaterEntries)
            .HasForeignKey(w => w.UserId);
    }
}