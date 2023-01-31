using AutoMapper;
using DataAccessLayer.Abstraction.Interfaces;
using DataAccessLayer.Data;
using Entities.Entity;
using Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Core;

namespace DataAccessLayer.Repository;

public class FarmRepository : IFarmRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;
    private readonly DbSet<Farm> _dbSetFarms;
    private readonly DbSet<UserFriend> _dbSetUserFriends;
    public FarmRepository(IMapper mapper,
            ApplicationContext context)
    {
        _mapper = mapper;
        _context = context;
        _dbSetFarms = context.Set<Farm>();
        _dbSetUserFriends = context.Set<UserFriend>();
    }
    public async Task CreateAsync(User user, FarmDto farmDto)
    {
        var farm = _mapper.Map<Farm>(farmDto);

        farm.UserId = user.Id;

        await _dbSetFarms.AddAsync(farm);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        var farm = await _dbSetFarms.FirstOrDefaultAsync(x => x.UserId == user.Id);

        if (farm is not null)
        {
            _dbSetFarms.Remove(farm);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<FarmDto?> GetByNameAsync(string name)
    {
        var farm = await _dbSetFarms.AsNoTracking()
            .Include(x => x.Pets)
            .FirstOrDefaultAsync(x => x.Name == name);

        if (farm is null) return null;

        var farmDto = _mapper .Map<FarmDto>(farm);

        return farmDto;
    }

    public async Task<FarmDto?> GetByIdAsync(Guid id)
    {
        var farm = await _dbSetFarms.AsNoTracking()
            .Include(x => x.Pets)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (farm is null) return null;

        var farmDto = _mapper.Map<FarmDto>(farm);

        return farmDto;
    }

    public async Task<List<FarmDto>?> GetChunkAsync(Guid userId, int number, int size)
    {
        var userFriends = await _dbSetUserFriends.Where(x => (x.UserId == userId 
            || x.FriendId == userId) && x.IsConfirmed)
            .Select(x => new Guid(x.UserId == userId ? x.FriendId.ToByteArray() : x.UserId.ToByteArray()))
            .ToListAsync();

        var farms = await _dbSetFarms.Where(x => userFriends.Any(c => c == x.UserId)).ToListAsync();

        if (farms is null) return null;

        var farmsDto = _mapper.Map<List<FarmDto>>(farms);

        return await Task.FromResult(farmsDto);
    }

    public async Task UpdateAsync(User user, FarmDto farmDto)
    {
        var farm = await _dbSetFarms.FirstOrDefaultAsync(x => x.UserId == user.Id);

        if (farm is not null)
        {
            var mapFarm = _mapper.Map<Farm>(farmDto);

            farm!.Name = mapFarm.Name;

            await _context.SaveChangesAsync();
        }
    }
}
