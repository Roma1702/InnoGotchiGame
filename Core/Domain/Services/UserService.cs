using Core.Abstraction.Interfaces;
using DataAccessLayer.Abstraction.Interfaces;
using FluentValidation;
using Models.Core;

namespace Core.Domain.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserFriendRepository _userFriendRepository;
    private readonly IValidator<UserDto> _userValidator;
    private readonly IValidator<ChangePasswordDto> _passwordValidator;
    private readonly IValidator<ShortUserDto> _shortUserValidator;

    public UserService(IUserRepository userRepository,
        IUserFriendRepository userFriendRepository,
        IValidator<UserDto> userValidator,
        IValidator<ChangePasswordDto> passwordValidator,
        IValidator<ShortUserDto> shortUserValidator)
    {
        _userRepository = userRepository;
        _userFriendRepository = userFriendRepository;
        _userValidator = userValidator;
        _passwordValidator = passwordValidator;
        _shortUserValidator = shortUserValidator;
    }

    public async Task UpdatePasswordAsync(Guid id, ChangePasswordDto changePasswordDto)
    {
        var passwordModel = await _passwordValidator.ValidateAsync(changePasswordDto);

        if (passwordModel.IsValid)
        {
            await _userRepository.UpdatePasswordAsync(id, changePasswordDto);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id);
    }

    public async Task<ShortUserDto?> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        return user;
    }

    public async Task<ShortUserDto?> GetByNameAsync(string name)
    {
        var user = await _userRepository.GetByNameAsync(name);

        return user;
    }

    public async Task<IEnumerable<ShortUserDto>?> GetChunkAsync(Guid userId, int number, int size)
    {
        var requests = await _userFriendRepository.GetRequestsId(userId);

        var users = await _userRepository.GetChunkAsync(requests!, number, size);

        return users;
    }

    public async Task SignUpAsync(UserDto userDto)
    {
        var user = await _userValidator.ValidateAsync(userDto);

        if (user.IsValid)
        {
            await _userRepository.CreateAsync(userDto);
        }
    }

    public async Task UpdateAsync(Guid id, ShortUserDto userDto)
    {
        var result = await _userRepository.GetByIdAsync(id);

        if (result is not null)
        {
            var user = await _shortUserValidator.ValidateAsync(userDto);

            if (user.IsValid)
            {
                await _userRepository.UpdateAsync(id ,userDto);
            }
        }
    }
    public async Task InviteAsync(Guid userId, string friendName)
    {
        var friendDto = await _userRepository.GetByNameAsync(friendName);

        if (friendDto is not null)
        {
            var friend = await _userRepository.GetUserAsync(friendDto!);

            await _userFriendRepository.InviteAsync(userId, friend!.Id);
        }
    }
    public async Task ConfirmAsync(Guid userId, string friendName)
    {
        var friendDto = await _userRepository.GetByNameAsync(friendName);

        if (friendDto is not null)
        {
            var friend = await _userRepository.GetUserAsync(friendDto!);

            await _userFriendRepository.ConfirmAsync(userId, friend!.Id);
        }
    }

    public async Task RejectAsync(Guid userId, string friendName)
    {
        var friendDto = await _userRepository.GetByNameAsync(friendName);

        if (friendDto is not null)
        {
            var friend = await _userRepository.GetUserAsync(friendDto!);

            await _userFriendRepository.RejectAsync(userId, friend!.Id);
        }
    }
}
