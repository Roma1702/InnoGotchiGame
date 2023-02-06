using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configuration;

public class InnogotchiConfiguration : IEntityTypeConfiguration<Innogotchi>
{
    public void Configure(EntityTypeBuilder<Innogotchi> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasOne(x => x.InnogotchiState)
            .WithOne(x => x.Innogotchi)
            .HasForeignKey<InnogotchiState>(x => x.InnogotchiId);

        builder.HasMany(x => x.Parts)
            .WithOne(x => x.Innogotchi)
            .HasForeignKey(x => x.InnogotchiId);
    }
}
