using AutoMapper;
using DataAccessLayer.Abstraction.Interfaces;
using DataAccessLayer.Data;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Models.Core;

namespace DataAccessLayer.Repository;

public class InnogotchiStateRepository : IInnogotchiStateRepository
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly DbSet<InnogotchiState> _dbSet;

    public InnogotchiStateRepository(ApplicationContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<InnogotchiState>();
    }
    public async Task CreateAsync(InnogotchiDto innogotchiDto)
    {
        var innogotchi = _mapper.Map<Innogotchi?>(innogotchiDto);

        InnogotchiState state = new() { Innogotchi = innogotchi, Created = DateTimeOffset.Now };

        await _dbSet.AddAsync(state);

        await _context.SaveChangesAsync();
    }

    public async Task<InnogotchiStateDto?> GetByIdAsync(Guid id)
    {
        var state = await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (state is null) return null;

        var stateDto = _mapper.Map<InnogotchiStateDto?>(state);

        return stateDto;
    }

    public async Task UpdateAsync(InnogotchiStateDto stateDto)
    {
        var state = _mapper.Map<InnogotchiState>(stateDto);

        _context.Entry(state).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }
}
