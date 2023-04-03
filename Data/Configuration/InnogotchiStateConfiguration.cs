using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Contracts.Enum.Enums;

namespace DataAccessLayer.Configuration;

public class InnogotchiStateConfiguration : IEntityTypeConfiguration<InnogotchiState>
{
    public void Configure(EntityTypeBuilder<InnogotchiState> builder)
    {
        builder.Property(x => x.Hunger)
            .HasDefaultValue(HungerLevel.Normal);
        builder.Property(x => x.Thirsty)
            .HasDefaultValue(ThirstyLevel.Normal);
    }
}
