using Entities.Entity;
using Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasOne(x => x.Photo)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.PhotoId);

        builder.HasOne(x => x.Farm)
            .WithOne(x => x.User)
            .HasForeignKey<Farm>(x => x.UserId);
    }
}
