using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AquaTracker.Infrastructure.Water;

public class WaterConfiguration : IEntityTypeConfiguration<Domain.Water.WaterEntry>
{
    public void Configure(EntityTypeBuilder<Domain.Water.WaterEntry> builder)
    {
        // Указываем, что Id — это первичный ключ
        builder.HasKey(w => w.Id);

        // Поле Amount должно быть обязательным
        builder.Property(w => w.Amount)
            .IsRequired();

        // Поле TimeLogged также должно быть обязательным
        builder.Property(w => w.Time)
            .IsRequired();

        // Настройка связи с сущностью User
        builder.HasOne(w => w.User)
            .WithMany(u => u.WaterEntries)
            .HasForeignKey(w => w.UserId);
    }
}