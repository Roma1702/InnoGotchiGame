using AutoMapper;
using DataAccessLayer.Abstraction.Interfaces;
using DataAccessLayer.Data;
using Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Core;

namespace DataAccessLayer.Repository;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _dbSet;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;
    public UserRepository(ApplicationContext context,
        UserManager<User> userManager,
        IMapper mapper,
        IPasswordHasher<User> passwordHasher)
    {
        _dbSet = context.Set<User>();
        _userManager = userManager;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task CreateAsync(UserDto userDto)
    {
        var isAlreadyExists = await _dbSet.AsNoTracking().
            FirstOrDefaultAsync(x => x.NormalizedEmail == userDto.Email ||
                                     x.NormalizedUserName == $"{userDto.Name} {userDto.Surname}");

        if (isAlreadyExists is null)
        {
            var user = _mapper.Map<User>(userDto);

            user.SecurityStamp = Guid.NewGuid().ToString("D");

            var result = await _userManager.CreateAsync(user);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _dbSet.FindAsync(id);

        if (user is not null)
        {
            await _userManager.DeleteAsync(user);
        }
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _dbSet.AsNoTracking()
            .Include(x => x.Photo)
            .Include(x => x.Farm)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user is null) return null;

        var userDto = _mapper.Map<UserDto>(user);

        return userDto;
    }

    public async Task<List<UserDto>> GetChunkAsync(int size, int number)
    {
        var users = await _dbSet.AsNoTracking()
            .Include(x => x.Photo)
            .Include(x => x.Farm)
            .Skip(number * size)
            .Take(number)
            .ToListAsync();

        var userDto = _mapper.Map<List<UserDto>>(users);

        return userDto;
    }

    public async Task UpdateAsync(UserDto userDto)
    {
        var user = await _dbSet.FindAsync(userDto.Id);

        if (user is not null)
        {
            var mapUser = _mapper.Map<User>(userDto);

            user.UserName = mapUser.UserName;
            user.Photo = mapUser.Photo;

            await _userManager.UpdateAsync(user);
        }
    }

    public async Task UpdatePasswordAsync(Guid userId, string oldPassword, string newPassword)
    {
        var user = await _dbSet.FindAsync(userId);

        if (user is not null)
        {
            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword);

            if (verificationResult == PasswordVerificationResult.Success)
            {
                // var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
