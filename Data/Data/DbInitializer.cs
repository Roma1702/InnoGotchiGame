using DataAccessLayer.Abstraction.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataAccessLayer.Data;

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationContext _context;
	public DbInitializer(ApplicationContext context)
	{
		_context= context;
	}

    public void Initialize()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _context.Roles.AddRange(FakeData.Roles);
        _context.SaveChanges();

        _context.MediaUsers!.AddRange(FakeData.MediaUsers);
        _context.SaveChanges();

        _context.MediaInnogotchiParts!.AddRange(FakeData.InnogotchiParts);
        _context.SaveChanges();

        _context.Users!.AddRange(FakeData.Users);
        _context.SaveChanges();

        _context.UserRoles!.AddRange(FakeData.UserRoles);
        _context.SaveChanges();

        _context.Pets!.AddRange(FakeData.Pets);
        _context.SaveChanges();

        _context.InnogotchiStates!.AddRange(FakeData.InnogotchiStates);
        _context.SaveChanges();

        _context.Farms!.AddRange(FakeData.Farms);
        _context.SaveChanges();

        _context.UserFriends!.AddRange(FakeData.UserFriends);
        _context.SaveChanges();
    }
}
