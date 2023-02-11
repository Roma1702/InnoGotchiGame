using Contracts.DTO;
using Core.Domain.Services;
using DataAccessLayer.Abstraction.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models.Core;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace InnogotchiTests;

public class FarmServiceTests
{
    private readonly FarmService _sut;
    private readonly Mock<IFarmRepository> _farmRepositoryMock = new();
    private readonly Mock<IUserFriendRepository> _userFriendRepositoryMock = new();
    private readonly Mock<IValidator<FarmDto>> _validatorMock = new();
    private Fixture _fixture;

    public FarmServiceTests()
    {
        _sut = new(_farmRepositoryMock.Object,
            _userFriendRepositoryMock.Object,
            _validatorMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task GetUserFarmsAsync_WhenYouHaveFriends()
    {
        var userId = _fixture.Create<Guid>();
        var userFriends = _fixture.CreateMany<Guid>(1).ToList();
        var friendsFarms = _fixture.CreateMany<FarmDto>(1).ToList();
        int number = 0;
        int size = 1;
        _userFriendRepositoryMock.Setup(x => x.GetFriendsId(userId))
            .ReturnsAsync(userFriends);
        _farmRepositoryMock.Setup(x => x.GetChunkAsync(userFriends, number, size))
            .ReturnsAsync(friendsFarms);

        var farms = await _sut.GetChunkAsync(userId, number, size);

        farms.Should().NotBeNull()
            .And.HaveCount(1);
        farms![0].Should().Be(friendsFarms[0]);
    }

    [Fact]
    public async Task GetUserFarmsAsync_WhenYouDoesNotHaveFriends()
    {
        var userId = _fixture.Create<Guid>();
        var userFriends = new List<Guid>();
        int number = 0;
        int size = 1;
        _userFriendRepositoryMock.Setup(x => x.GetFriendsId(userId))
            .ReturnsAsync(() => null);
        _farmRepositoryMock.Setup(x => x.GetChunkAsync(userFriends, number, size))
            .ReturnsAsync(() => null);

        var farms = await _sut.GetChunkAsync(userId, number, size);

        farms.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsyncAsync_ShouldReturnFarm_WhenFarmExists()
    {
        var farmDto = _fixture.Create<FarmDto>();
        var userId = _fixture.Create<Guid>();
        _farmRepositoryMock.Setup(x => x.GetByIdAsync(userId))
            .ReturnsAsync(farmDto);

        var farm = await _sut.GetByIdAsync(userId);

        farm?.Name.Should().Be(farmDto.Name)
            .And.NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsyncAsync_ShouldReturnNothing_WhenFarmDoesNotExists()
    {
        _farmRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(() => null);

        var farm = await _sut.GetByIdAsync(Guid.NewGuid());

        farm.Should().BeNull();
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnFarm_WhenFarmExists()
    {
        var farmDto = _fixture.Create<FarmDto>();
        _farmRepositoryMock.Setup(x => x.GetByNameAsync(farmDto.Name!))
            .ReturnsAsync(farmDto);

        var farm = await _sut.GetByNameAsync(farmDto.Name!);

        farm?.Name.Should().Be(farmDto.Name)
            .And.NotBeNull();
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnNothing_WhenFarmDoesNotExists()
    {
        _farmRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(() => null);

        var farm = await _sut.GetByNameAsync(Guid.NewGuid().ToString());

        farm.Should().BeNull();
    }

    [Fact]
    public async Task CreateFarmAsync_WhenFarmIsValid_ShouldReturnOkResult()
    {
        var farmDto = _fixture.Create<FarmDto>();
        var userId = _fixture.Create<Guid>();
        var validationResult = new ValidationResult() { Errors = { } };
        _validatorMock.Setup(x => x.Validate(farmDto))
            .Returns(validationResult);
        _farmRepositoryMock.Setup(x => x.UpdateAsync(userId, farmDto));
        
        var result = await _sut.UpdateAsync(userId, farmDto);
        var okResult = result as OkResult;

        _farmRepositoryMock.Verify(x => x.UpdateAsync(userId, farmDto), Times.Once);
        result.Should().Be(okResult);
    }

    [Fact]
    public async Task GetFarmStatisticAsync()
    {
        var userId = _fixture.Create<Guid>();
        var countOfAlivePets = _fixture.Create<int>();
        var countOfDeadPets = _fixture.Create<int>();
        var averageFeedPeriod = _fixture.Create<double>();
        var averageDrinking = _fixture.Create<double>();
        var averageHappinessDays = _fixture.Create<double>();
        var averageAge = _fixture.Create<double>();
        _farmRepositoryMock.Setup(x => x.GetCountOfAliveAsync(userId))
            .ReturnsAsync(countOfAlivePets);
        _farmRepositoryMock.Setup(x => x.GetCountOfDeadAsync(userId))
            .ReturnsAsync(countOfDeadPets);
        _farmRepositoryMock.Setup(x => x.GetAverageFeedPeriodAsync(userId))
            .ReturnsAsync(averageFeedPeriod);
        _farmRepositoryMock.Setup(x => x.GetAverageDrinkPeriodAsync(userId))
            .ReturnsAsync(averageDrinking);
        _farmRepositoryMock.Setup(x => x.GetAverageHappinessDaysCount(userId))
            .ReturnsAsync(averageHappinessDays);
        _farmRepositoryMock.Setup(x => x.GetAverageAgeAsync(userId))
            .ReturnsAsync(averageAge);
        FarmStatisticDto farmStatisticDto = new()
        {
            CountOfAlivePets = countOfAlivePets,
            CountOfDeadPets = countOfDeadPets,
            AverageFeedPeriod = averageFeedPeriod,
            AverageDrinking = averageDrinking,
            AverageHappinessDays = averageHappinessDays,
            AverageAge = averageAge
        };

        var statistic = await _sut.GetFarmStatistic(userId);

        statistic!.CountOfAlivePets.Should().Be(farmStatisticDto.CountOfAlivePets);
        statistic!.CountOfDeadPets.Should().Be(farmStatisticDto.CountOfDeadPets);
        statistic!.AverageFeedPeriod.Should().Be(farmStatisticDto.AverageFeedPeriod);
        statistic!.AverageDrinking.Should().Be(farmStatisticDto.AverageDrinking);
        statistic!.AverageHappinessDays.Should().Be(farmStatisticDto.AverageHappinessDays);
        statistic!.AverageAge.Should().Be(farmStatisticDto.AverageAge);
    }

    [Fact]
    public async Task UpdateFarmAsync_WhenFarmIsValid_ShouldReturnOkResult()
    {
        var farmDto = _fixture.Create<FarmDto>();
        var userId = _fixture.Create<Guid>();
        var validationResult = new ValidationResult() { Errors = { } };
        _validatorMock.Setup(x => x.Validate(farmDto))
            .Returns(validationResult);
        _farmRepositoryMock.Setup(x => x.CreateAsync(userId, farmDto));

        var result = await _sut.CreateAsync(userId, farmDto);
        var okResult = result as OkResult;

        _farmRepositoryMock.Verify(x => x.CreateAsync(userId, farmDto), Times.Once);
        result.Should().Be(okResult);
    }

    [Fact]
    public async Task DeleteFarmAsync_WhenFarmExists()
    {
        var userId = _fixture.Create<Guid>();
        _farmRepositoryMock.Setup(x => x.DeleteAsync(userId));

        var result = await _sut.DeleteAsync(userId);
        var okResult = result as OkResult;

        _farmRepositoryMock.Verify(x => x.DeleteAsync(userId), Times.Once);
        result.Should().Be(okResult);
    }
}
