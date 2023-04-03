using Contracts.DTO;
using Core.Abstraction.Interfaces;
using DataAccessLayer.Abstraction.Interfaces;
using Entities.Identity;
using FluentValidation;
using Models.Core;

namespace Core.Domain.Services;

public class InnogotchiService : IInnogotchiService
{
    private readonly IValidator<InnogotchiDto> _innogotchiValidator;
    private readonly IInnogotchiRepository _innogotchiRepository;
    private readonly IUserRepository _userRepository;

    public InnogotchiService(IValidator<InnogotchiDto> innogotchiValidator,
        IInnogotchiRepository innogotchiRepository,
        IUserRepository userRepository)
    {
        _innogotchiValidator = innogotchiValidator;
        _innogotchiRepository = innogotchiRepository;
        _userRepository = userRepository;
    }
    public async Task CreateAsync(Guid userId, InnogotchiDto dto)
    {
        var innogotchi = await _innogotchiValidator.ValidateAsync(dto);

        var userDto = await _userRepository.GetByIdAsync(userId);

        if (innogotchi.IsValid)
        {
            var user = await _userRepository.GetUserAsync(userDto!);

            await _innogotchiRepository.CreateAsync(user!.Farm!.Id, dto);
        }
    }

    public async Task DeleteAsync(string name)
    {
        await _innogotchiRepository.DeleteAsync(name);
    }

    public async Task<InnogotchiDto?> GetByNameAsync(Guid userId, string name)
    {
        var user = await GetOwnerAsync(userId);

        var innogotchi = await _innogotchiRepository.GetByNameAsync(user!.Farm!.Id, name);

        return innogotchi;
    }

    public async Task<PetInfoDto?> GetStateByNameAsync(Guid userId, string name)
    {
        var user = await GetOwnerAsync(userId);

        var innogotchi = await _innogotchiRepository.GetStateByNameAsync(user!.Farm!.Id, name);

        return innogotchi;
    }

    public async Task<int> GetCountAsync(Guid userId)
    {
        var user = await GetOwnerAsync(userId);

        if (user!.Farm is null)
        {
            return 0;
        }

        var count = await _innogotchiRepository.GetCountAsync(user!.Farm!.Id);

        return count;
    }

    public async Task<IEnumerable<InnogotchiDto>?> GetChunkAsync(Guid userId, int number, int size)
    {
        var user = await GetOwnerAsync(userId);

        if (user!.Farm is null) return null;

        var pets = await _innogotchiRepository.GetChunkAsync(user!.Farm!.Id, number, size);

        return pets;
    }

    public async Task<IEnumerable<PetInfoDto>?> SortByHappinessDays(Guid userId, int number, int size)
    {
        var user = await GetOwnerAsync(userId);

        if (user!.Farm is null) return null;

        var pets = await _innogotchiRepository.SortByHappinessDays(user!.Farm!.Id, number, size);

        return pets;
    }

    public async Task<IEnumerable<PetInfoDto>?> SortByAgeAsync(Guid userId, int number, int size)
    {
        var user = await GetOwnerAsync(userId);

        if (user!.Farm is null) return null;

        var pets = await _innogotchiRepository.SortByAgeAsync(user!.Farm!.Id, number, size);

        return pets;
    }

    public async Task<IEnumerable<PetInfoDto>?> SortByHungerLevelAsync(Guid userId, int number, int size)
    {
        var user = await GetOwnerAsync(userId);

        if (user!.Farm is null) return null;

        var pets = await _innogotchiRepository.SortByHungerLevelAsync(user!.Farm!.Id, number, size);

        return pets;
    }

    public async Task<IEnumerable<PetInfoDto>?> SortByWaterLevelAsync(Guid userId, int number, int size)
    {
        var user = await GetOwnerAsync(userId);

        if (user!.Farm is null) return null;

        var pets = await _innogotchiRepository.SortByWaterLevelAsync(user!.Farm!.Id, number, size);

        return pets;
    }

    public async Task UpdateAsync(InnogotchiDto dto)
    {
        var innogotchi = await _innogotchiValidator.ValidateAsync(dto);

        if (innogotchi.IsValid)
        {
            await _innogotchiRepository.UpdateAsync(dto);
        }
    }

    private async Task<User> GetOwnerAsync(Guid userId)
    {
        var userDto = await _userRepository.GetByIdAsync(userId);

        var user = await _userRepository.GetUserAsync(userDto!);

        return user!;
    } 
}
