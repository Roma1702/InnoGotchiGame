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

        builder.HasOne(x => x.Body)
            .WithMany(x => x.Bodies)
            .HasForeignKey(x => x.BodyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Nose)
            .WithMany(x => x.Noses)
            .HasForeignKey(x => x.NoseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Eyes)
            .WithMany(x => x.Eyes)
            .HasForeignKey(x => x.EyesId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Mouth)
            .WithMany(x => x.Mouths)
            .HasForeignKey(x => x.MouthId);
    }
}
