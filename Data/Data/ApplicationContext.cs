using Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Entities.Entity;
using System.Reflection;

namespace DataAccessLayer.Data
{
    public class ApplicationContext : IdentityDbContext<User,
    IdentityRole<Guid>,
    Guid,
    IdentityUserClaim<Guid>,
    IdentityUserRole<Guid>,
    IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>,
    IdentityUserToken<Guid>>
    {
        public DbSet<Farm>? Farms { get; set; }
        public DbSet<Innogotchi>? Pets { get; set; }
        public DbSet<InnogotchiPart>? InnogotchiParts { get; set; }
        public DbSet<InnogotchiState>? InnogotchiStates { get; set; }
        public DbSet<UserFriend>? UserFriends { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
