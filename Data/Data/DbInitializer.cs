using DataAccessLayer.Abstraction.Interfaces;

namespace DataAccessLayer.Data;

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationContext _context;
	public DbInitializer(ApplicationContext context)
	{
		_context = context;
	}

    public void Initialize()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _context.Users?.AddRange(FakeData.Users);
        _context.SaveChanges();

        _context.Farms?.AddRange(FakeData.Farms);
        _context.SaveChanges();

        _context.Roles?.AddRange(FakeData.Roles);
        _context.SaveChanges();

        _context.UserRoles?.AddRange(FakeData.UserRoles);
        _context.SaveChanges();

        _context.Pets?.AddRange(FakeData.Pets);
        _context.SaveChanges();

        _context.InnogotchiParts?.AddRange(FakeData.InnogotchiParts);
        _context.SaveChanges();

        _context.InnogotchiStates?.AddRange(FakeData.InnogotchiStates);
        _context.SaveChanges();
    }
}
