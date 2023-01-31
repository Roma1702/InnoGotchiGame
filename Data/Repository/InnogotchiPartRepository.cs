using AutoMapper;
using DataAccessLayer.Abstraction.Interfaces;
using DataAccessLayer.Data;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Models.Core;

namespace DataAccessLayer.Repository;

public class InnogotchiPartRepository : IInnogotchiPartRepository
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly DbSet<InnogotchiPart> _dbSet;
    public InnogotchiPartRepository(ApplicationContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<InnogotchiPart>();
    }
    public async Task CreateAsync(MediaDto mediaPartDto)
    {
        var mediaPart = _mapper.Map<InnogotchiPart>(mediaPartDto);

        byte[]? imageData = null;

        using (var binaryReader = new BinaryReader(mediaPartDto.Image!.OpenReadStream()))
        {
            imageData = binaryReader.ReadBytes((int)mediaPartDto.Image!.Length);
        }

        mediaPart.Image = imageData;

        await _context.SaveChangesAsync();
    }
}
