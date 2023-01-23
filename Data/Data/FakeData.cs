using Entities.Entity;
using Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Data;

public static class FakeData
{
    public static ICollection<MediaUser> MediaUsers = new List<MediaUser>
    {
        new MediaUser
        {
            Data = File.ReadAllBytes(@"Images/Profile/avatar.jpg")
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

    public static ICollection<User> Users = new List<User>
    {
        new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@aaa.com",
            NormalizedEmail = "admin@aaa.com",
            UserName = "Ivan Ivanov",
            NormalizedUserName = "Ivan Ivanov",
            EmailConfirmed = true,
            PasswordHash =
                "AQAAAAEAACcQAAAAEHOnF+aiX0aOAcQTNVLA4BNSmJ3aJVLcgq4JtmUakxr/xYQs9CPHyZwRJ9iK2MJfQg==", // !QAZ2wsx
            SecurityStamp = Guid.NewGuid().ToString("D"),
            Photo = MediaUsers.First()
        },
        new User
        {
            Id = Guid.NewGuid(),
            Email = "user@aaa.com",
            NormalizedEmail = "user@aaa.com",
            UserName = "Petr Petrov",
            NormalizedUserName = "Petr Petrov",
            EmailConfirmed = true,
            PasswordHash =
                "AQAAAAEAACcQAAAAEHOnF+aiX0aOAcQTNVLA4BNSmJ3aJVLcgq4JtmUakxr/xYQs9CPHyZwRJ9iK2MJfQg==", // !QAZ2wsx
            SecurityStamp = Guid.NewGuid().ToString("D"),
            Photo = MediaUsers.First()
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

    public static ICollection<MediaInnogotchiPart> InnogotchiParts = new List<MediaInnogotchiPart>
    {
        new MediaInnogotchiPart
        {
            Data = File.ReadAllBytes(@"Images/Bodies/body1.svg"),
            PartType = InnogotchiPartType.Body
        },
        new MediaInnogotchiPart
        {
            Data = File.ReadAllBytes(@"Images/Eyes/eyes1.svg"),
            PartType = InnogotchiPartType.Eyes
        },
        new MediaInnogotchiPart
        {
            Data = File.ReadAllBytes(@"Images/Mouths/mouth1.svg"),
            PartType = InnogotchiPartType.Mouth
        },
        new MediaInnogotchiPart
        {
            Data = File.ReadAllBytes(@"Images/Noses/nose1.svg"),
            PartType = InnogotchiPartType.Nose
        }
    };

    public static ICollection<InnogotchiState> InnogotchiStates = new List<InnogotchiState>
    {
        new InnogotchiState
        {
            Age = 1,
            Hunger = InnogotchiState.HungerLevel.Full,
            Thirsty = InnogotchiState.ThirstyLevel.Full,
            HappyDays = 2,
            Innogotchi = Pets!.First()
        }
    };

    public static ICollection<Innogotchi> Pets = new List<Innogotchi>
    {
        new Innogotchi
        {
            Name = "Perry",
            Body = InnogotchiParts.First(x => x.PartType == InnogotchiPartType.Body),
            Nose = InnogotchiParts.First(x => x.PartType == InnogotchiPartType.Nose),
            Eyes = InnogotchiParts.First(x => x.PartType == InnogotchiPartType.Eyes),
            Mouth = InnogotchiParts.First(x => x.PartType == InnogotchiPartType.Mouth),
            Farm = Farms!.First()
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

    public static ICollection<UserFriend> UserFriends = new List<UserFriend>
    {
        new UserFriend
        {
            UserId = Users.Last().Id,
            FriendId = Users.First().Id,
            IsVerified = true
        }
    };
}
