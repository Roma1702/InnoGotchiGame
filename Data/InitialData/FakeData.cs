using Entities.Entity;
using Entities.Identity;
using Microsoft.AspNetCore.Identity;
using static Contracts.Enum.Enums;

namespace DataAccessLayer.Data;

public static class FakeData
{
    public static ICollection<User> Users = new List<User>
    {
        new User
        {
            Email = "admin@aaa.com",
            NormalizedEmail = "admin@aaa.com",
            UserName = "Ivan Ivanov",
            NormalizedUserName = "Ivan Ivanov",
            EmailConfirmed = true,
            PasswordHash =
                "AQAAAAEAACcQAAAAEHOnF+aiX0aOAcQTNVLA4BNSmJ3aJVLcgq4JtmUakxr/xYQs9CPHyZwRJ9iK2MJfQg==", // !QAZ2wsx
            SecurityStamp = Guid.NewGuid().ToString("D"),
            ProfilePhoto =  File.ReadAllBytes(@"wwwroot/Images/Profile/avatar.jpg")
        },
        new User
        {
            Email = "user@aaa.com",
            NormalizedEmail = "user@aaa.com",
            UserName = "Petr Petrov",
            NormalizedUserName = "Petr Petrov",
            EmailConfirmed = true,
            PasswordHash =
                "AQAAAAEAACcQAAAAEHOnF+aiX0aOAcQTNVLA4BNSmJ3aJVLcgq4JtmUakxr/xYQs9CPHyZwRJ9iK2MJfQg==", // !QAZ2wsx
            SecurityStamp = Guid.NewGuid().ToString("D"),
            ProfilePhoto =  File.ReadAllBytes(@"wwwroot/Images/Profile/avatar.jpg")
        }
    };

    public static ICollection<Farm> Farms = new List<Farm>
    {
        new Farm
        {
            Name = "Farm1",
            User = Users.First()
        }
    };

    public static ICollection<Innogotchi> Pets = new List<Innogotchi>
    {
        new Innogotchi
        {
            Name = "Perry",
            Farm = Farms?.First()
        }
    };

    public static ICollection<InnogotchiPart> InnogotchiParts = new List<InnogotchiPart>()
    {
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Bodies/body1.svg"),
            PartType = PartType.Body,
            Innogotchi = Pets.First()
        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Eyes/eyes1.svg"),
            PartType = PartType.Eyes,
            Innogotchi = Pets.First()
        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Mouths/mouth1.svg"),
            PartType = PartType.Mouth,
            Innogotchi = Pets.First()
        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Noses/nose1.svg"),
            PartType = PartType.Nose,
            Innogotchi = Pets.First()
        }
    };

    public static ICollection<IdentityRole<Guid>> Roles = new List<IdentityRole<Guid>>
    {
        new IdentityRole<Guid>
        {
            Id = Guid.NewGuid(),
            Name = "admin",
            NormalizedName = "admin"
        },
        new IdentityRole<Guid>
        {
            Id = Guid.NewGuid(),
            Name = "user",
            NormalizedName = "user"
        }
    };

    public static ICollection<IdentityUserRole<Guid>> UserRoles = new List<IdentityUserRole<Guid>>
    {
        new IdentityUserRole<Guid>
        {
            UserId = Users.First().Id,
            RoleId = Roles.First().Id
        },
        new IdentityUserRole<Guid>
        {
            UserId = Users.Last().Id,
            RoleId = Roles.Last().Id
        }
    };

    public static ICollection<InnogotchiState> InnogotchiStates = new List<InnogotchiState>
    {
        new InnogotchiState
        {
            Age = 0,
            Hunger = HungerLevel.Normal,
            Thirsty = ThirstyLevel.Normal,
            StartOfHappinessDays = DateTimeOffset.Now.AddDays(-2),
            HappinessDays = 0,
            Created = DateTimeOffset.Now.AddDays(-2),
            Innogotchi = Pets!.First()
        }
    };
}
