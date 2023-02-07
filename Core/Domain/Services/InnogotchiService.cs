using Contracts.DTO;
using Core.Abstraction.Interfaces;
using DataAccessLayer.Abstraction.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models.Core;

namespace Core.Domain.Services;

public class InnogotchiService : IInnogotchiService
{
    private readonly IValidator<InnogotchiDto> _innogotchiValidator;
    private readonly IInnogotchiRepository _innogotchiRepository;
    private readonly IUserRepository _userRepository;
    private readonly IInnogotchiStateRepository _innogotchiStateRepository;

    public InnogotchiService(IValidator<InnogotchiDto> innogotchiValidator,
        IInnogotchiRepository innogotchiRepository,
        IUserRepository userRepository,
        IInnogotchiStateRepository innogotchiStateRepository)
    {
        _innogotchiValidator = innogotchiValidator;
        _innogotchiRepository = innogotchiRepository;
        _userRepository = userRepository;
        _innogotchiStateRepository = innogotchiStateRepository;
    }
    public async Task<IActionResult> CreateAsync(Guid userId, InnogotchiDto dto)
    {
        var innogotchi = await _innogotchiValidator.ValidateAsync(dto);

        var userDto = await _userRepository.GetByIdAsync(userId);

        if (innogotchi.IsValid)
        {
            var user = await _userRepository.GetUserAsync(userDto!);

            await _innogotchiRepository.CreateAsync(user!.Farm!.Id, dto);

            await _innogotchiStateRepository.CreateAsync(dto);

            return new OkResult();
        }

        return new BadRequestResult();
    }

    public async Task<IActionResult> DeleteAsync(string name)
    {
        await _innogotchiRepository.DeleteAsync(name);

        return new OkResult();
    }

    public async Task<InnogotchiDto?> GetByNameAsync(Guid userId, string name)
    {
        var userDto = await _userRepository.GetByIdAsync(userId);

        var user = await _userRepository.GetUserAsync(userDto!);

        var innogotchi = await _innogotchiRepository.GetByNameAsync(user!.Farm!.Id, name);

        return innogotchi;
    }

    public async Task<List<PetInfoDto>?> GetChunkAsync(Guid userId, int number, int size)
    {
        var userDto = await _userRepository.GetByIdAsync(userId);

        var user = await _userRepository.GetUserAsync(userDto!);

        var pets = await _innogotchiRepository.GetChunkAsync(user!.Farm!.Id, number, size);

        return pets;
    }

    public async Task<List<PetInfoDto>?> SortByAgeAsync(Guid userId, int number, int size)
    {
        var userDto = await _userRepository.GetByIdAsync(userId);

        var user = await _userRepository.GetUserAsync(userDto!);

        var pets = await _innogotchiRepository.SortByAgeAsync(user!.Farm!.Id, number, size);

        return pets;
    }

    public async Task<List<PetInfoDto>?> SortByHungerLevelAsync(Guid userId, int number, int size)
    {
        var userDto = await _userRepository.GetByIdAsync(userId);

        var user = await _userRepository.GetUserAsync(userDto!);

        var pets = await _innogotchiRepository.SortByHungerLevelAsync(user!.Farm!.Id, number, size);

        return pets;
    }

    public async Task<List<PetInfoDto>?> SortByWaterLevelAsync(Guid userId, int number, int size)
    {
        var userDto = await _userRepository.GetByIdAsync(userId);

        var user = await _userRepository.GetUserAsync(userDto!);

        var pets = await _innogotchiRepository.SortByWaterLevelAsync(user!.Farm!.Id, number, size);

        return pets;
    }


    public async Task<IActionResult> UpdateAsync(InnogotchiDto dto)
    {
        var innogotchi = await _innogotchiValidator.ValidateAsync(dto);

        if (innogotchi.IsValid)
        {
            await _innogotchiRepository.UpdateAsync(dto);

            return new OkResult();
        }

        return new BadRequestResult();
    }
}
