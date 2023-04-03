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
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly DbSet<User> _dbSet;
    public UserRepository(ApplicationContext context,
        UserManager<User> userManager,
        IMapper mapper,
        IPasswordHasher<User> passwordHasher)
    {
        _userManager = userManager;
        _mapper = mapper;
        _dbSet = context.Set<User>();
        _passwordHasher = passwordHasher;
    }

    public async Task CreateAsync(UserDto userDto)
    {
        var isAlreadyExists = await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(x => x.NormalizedEmail == userDto.Email ||
                                     x.NormalizedUserName == $"{userDto.Name} {userDto.Surname}");

        if (isAlreadyExists is null)
        {
            var user = _mapper.Map<User>(userDto);

            user.SecurityStamp = Guid.NewGuid().ToString("D");

            await _userManager.CreateAsync(user, userDto.Password);
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

    public async Task<ShortUserDto?> GetByIdAsync(Guid id)
    {
        var user = await _dbSet.AsNoTracking()
            .Include(x => x.Farm)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user is null) return null;

        var userDto = _mapper.Map<ShortUserDto>(user);

        return userDto;
    }

    public async Task<ShortUserDto?> GetByNameAsync(string name)
    {
        var user = await _dbSet.AsNoTracking()
            .Include(x => x.Farm)
            .FirstOrDefaultAsync(x => x.UserName == name);

        var userDto = _mapper.Map<ShortUserDto>(user);

        return userDto;
    }

    public async Task<IEnumerable<ShortUserDto>?> GetRequestsAsync(IEnumerable<Guid> requestsId)
    {
        var users = await _dbSet.AsNoTracking()
            .Include(x => x.Farm)
            .Where(x => requestsId.Any(c => c == x.Id))
            .ToListAsync();

        if (users is null) return null;

        var userDto = _mapper.Map<List<ShortUserDto>>(users);

        return userDto;
    }

    public async Task UpdateAsync(ShortUserDto userDto)
    {
        var user = await _userManager.FindByEmailAsync(userDto.Email);

        if (user is not null)
        {
            user.UserName = $"{userDto.Name} {userDto.Surname}";

            user.ProfilePhoto = Convert.FromBase64String(userDto.ProfilePhoto!);

            user.FileExtension = userDto.FileExtension;

            user.SecurityStamp = Guid.NewGuid().ToString("D");

            await _userManager.UpdateAsync(user);
        }
    }

    public async Task UpdatePasswordAsync(Guid id, ChangePasswordDto changePasswordDto)
    {
        var user = await _dbSet.FindAsync(id);

        if (user is not null)
        {
            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash,
                changePasswordDto.CurrentPassword);

            if (verificationResult == PasswordVerificationResult.Success)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, changePasswordDto.NewPassword);
                await _userManager.UpdateAsync(user);
            }
        }
    }
    public async Task<User?> GetUserAsync(ShortUserDto userDto)
    {
        var mapUser = _mapper.Map<User>(userDto);

        var user = await _dbSet.AsNoTracking()
            .Include(x => x.Farm!)
            .ThenInclude(x => x.Pets)
            .FirstOrDefaultAsync(x => x.UserName == mapUser.UserName);

        return user;
    }
}
