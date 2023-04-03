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
            ProfilePhoto =  File.ReadAllBytes(@"wwwroot/Images/Profile/avatar.jpg"),
            FileExtension = "image/png"
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
            ProfilePhoto =  File.ReadAllBytes(@"wwwroot/Images/Profile/avatar.jpg"),
            FileExtension = "image/png"
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
        },
        new Innogotchi
        {
            Name = "Aboba",
            Farm = Farms?.First()
        },
        new Innogotchi
        {
            Name = "Finly",
            Farm = Farms?.First()
        }
    };

    public static ICollection<InnogotchiPart> InnogotchiParts = new List<InnogotchiPart>()
    {
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Bodies/body1.svg"),
            PartType = PartType.Body,
            Innogotchi = Pets.First(),
            FileExtension = "image/svg+xml"
        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Eyes/eyes1.svg"),
            PartType = PartType.Eyes,
            Innogotchi = Pets.First(),
            FileExtension = "image/svg+xml"

        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Mouths/mouth1.svg"),
            PartType = PartType.Mouth,
            Innogotchi = Pets.First(),
            FileExtension = "image/svg+xml"
        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Noses/nose1.svg"),
            PartType = PartType.Nose,
            Innogotchi = Pets.First(),
            FileExtension = "image/svg+xml"
        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Bodies/body2.svg"),
            PartType = PartType.Body,
            Innogotchi = Pets.Last(),
            FileExtension = "image/svg+xml"
        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Eyes/eyes2.svg"),
            PartType = PartType.Eyes,
            Innogotchi = Pets.Last(),
            FileExtension = "image/svg+xml"
        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Mouths/mouth2.svg"),
            PartType = PartType.Mouth,
            Innogotchi = Pets.Last(),
            FileExtension = "image/svg+xml"
        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Noses/nose2.svg"),
            PartType = PartType.Nose,
            Innogotchi = Pets.Last(),
            FileExtension = "image/svg+xml"
        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Bodies/body3.svg"),
            PartType = PartType.Body,
            Innogotchi = Pets.Skip(1).First(),
            FileExtension = "image/svg+xml"
        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Eyes/eyes3.svg"),
            PartType = PartType.Eyes,
            Innogotchi = Pets.Skip(1).First(),
            FileExtension = "image/svg+xml"
        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Mouths/mouth3.svg"),
            PartType = PartType.Mouth,
            Innogotchi = Pets.Skip(1).First(),
            FileExtension = "image/svg+xml"
        },
        new InnogotchiPart
        {
            Image = File.ReadAllBytes(@"wwwroot/Images/Noses/nose3.svg"),
            PartType = PartType.Nose,
            Innogotchi = Pets.Skip(1).First(),
            FileExtension = "image/svg+xml"
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
            Innogotchi = Pets!.First(),
            CountOfFeeds = 3,
            CountOfDrinks = 4
        },
        new InnogotchiState
        {
            Age = 3,
            Hunger = HungerLevel.Full,
            Thirsty = ThirstyLevel.Full,
            StartOfHappinessDays = DateTimeOffset.Now.AddDays(-1),
            HappinessDays = 3,
            Created = DateTimeOffset.Now.AddDays(-8),
            Innogotchi = Pets.Skip(1).First(),
            CountOfFeeds = 3,
            CountOfDrinks = 4
        },
        new InnogotchiState
        {
            Age = 1,
            Hunger = HungerLevel.Full,
            Thirsty = ThirstyLevel.Thirsty,
            StartOfHappinessDays = DateTimeOffset.Now.AddDays(-1),
            HappinessDays = 1,
            Created = DateTimeOffset.Now.AddDays(-8),
            Innogotchi = Pets!.Last(),
            CountOfFeeds = 3,
            CountOfDrinks = 4
        }
    };
}
