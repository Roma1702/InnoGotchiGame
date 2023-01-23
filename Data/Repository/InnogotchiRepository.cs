using AutoMapper;
using DataAccessLayer.Abstraction.Interfaces;
using DataAccessLayer.Data;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Models.Core;

namespace DataAccessLayer.Repository;

public class InnogotchiRepository : IInnogotchiRepository
{
    private readonly IMapper _mapper;
    private readonly ApplicationContext _context;
    private readonly DbSet<Innogotchi> _dbSet;
    public InnogotchiRepository(IMapper mapper,
        ApplicationContext context)
    {
        _mapper = mapper;
        _context = context;
        _dbSet = context.Set<Innogotchi>();
    }
    public async Task CreateAsync(InnogotchiDto innogotchiDto)
    {
        var innogotchi = _mapper.Map<Innogotchi>(innogotchiDto);

        await _dbSet.AddAsync(innogotchi);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var innogotchi = await _dbSet.FindAsync(id);

        if (innogotchi is not null)
        {
            _dbSet.Remove(innogotchi);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<InnogotchiDto> GetById(Guid id)
    {
        var innogotchi = await _dbSet.AsNoTracking()
            .Include(x => x.InnogotchiState)
            .FirstOrDefaultAsync(x => x.Id == id);

        var innogotchiDto = _mapper.Map<InnogotchiDto>(innogotchi);

        return innogotchiDto;
    }

    public async Task<List<InnogotchiDto>> GetChunkAsync(int number, int size)
    {
        var pets = await _dbSet.AsNoTracking()
            .Include(x => x.InnogotchiState)
            .Skip(number * size)
            .Take(size)
            .ToListAsync();

        var petsDto = _mapper.Map<List<InnogotchiDto>>(pets);

        return petsDto;
    }

    public async Task UpdateAsync(InnogotchiDto innogotchiDto)
    {
        var innogotchi = _mapper.Map<Innogotchi>(innogotchiDto);

        _context.Entry(innogotchi).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }
}
