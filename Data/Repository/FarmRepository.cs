using AutoMapper;
using DataAccessLayer.Abstraction.Interfaces;
using DataAccessLayer.Data;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Models.Core;

namespace DataAccessLayer.Repository;

public class FarmRepository : IFarmRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;
    private readonly DbSet<Farm> _dbSet;
    public FarmRepository(IMapper mapper,
            ApplicationContext context)
    {
        _mapper = mapper;
        _context = context;
        _dbSet = context.Set<Farm>();
    }
    public async Task CreateAsync(FarmDto farmDto)
    {
        var farm = _mapper.Map<Farm>(farmDto);

        farm.Pets?.Clear();

        await _dbSet.AddAsync(farm);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var farm = await _dbSet.FindAsync(id);

        if (farm is not null)
        {
            _dbSet.Remove(farm);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<FarmDto?> GetByIdAsync(Guid id)
    {
        var farm = await _dbSet.AsNoTracking()
            .Include(x => x.Pets)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (farm is null) return null;

        var farmDto = _mapper .Map<FarmDto>(farm);

        return farmDto;
    }

    public async Task<List<FarmDto>> GetChunkAsync(int size, int number)
    {
        var farms = await _dbSet.AsNoTracking()
            .Include(x => x.Pets)
            .Skip(number * size)
            .Take(size)
            .ToListAsync();

        var farmsDto = _mapper.Map<List<FarmDto>>(farms);

        return farmsDto;
    }

    public async Task UpdateAsync(FarmDto farmDto)
    {
        var farm = _mapper.Map<Farm>(farmDto);

        _context.Entry(farm).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }
}
