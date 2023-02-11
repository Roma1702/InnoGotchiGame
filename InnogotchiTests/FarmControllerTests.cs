using Contracts.DTO;
using Core.Abstraction.Interfaces;
using Entities.Entity;
using InnoGotchiGame.Controllers;
using Microsoft.AspNetCore.Mvc;
using Models.Core;

namespace InnogotchiTests;

public class FarmControllerTests
{
    private readonly FarmController _sut;
    private readonly Mock<IFarmService> _farmServiceMock = new();
    private readonly Mock<IIdentityService> _identityServiceMock = new();
    private Fixture _fixture;

    public FarmControllerTests()
    {
        _sut = new(_farmServiceMock.Object, _identityServiceMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task GetUserFriendFarmsAsync()
    {
        int number = 0;
        int size = 1;
        var userId = _fixture.Create<string>();
        var friendsFarms = _fixture.CreateMany<FarmDto>(1).ToList();
        _identityServiceMock.Setup(x => x.GetUserIdentity())
            .Returns(userId);
        _farmServiceMock.Setup(x => x.GetChunkAsync(Guid.Parse(userId), number, size))
            .ReturnsAsync(friendsFarms);

        var result = await _sut.GetChunkAsync(number, size);

        result.Should().NotBeNull()
            .And.HaveCount(1);
        result![0].Should().Be(friendsFarms[0]);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnFarm_WhenFarmExists()
    {
        var userId = _fixture.Create<string>();
        var farmDto = _fixture.Create<FarmDto>();
        _identityServiceMock.Setup(x => x.GetUserIdentity())
            .Returns(userId);
        _farmServiceMock.Setup(x => x.GetByIdAsync(Guid.Parse(userId)))
            .ReturnsAsync(farmDto);

        var farm = await _sut.GetByIdAsync();

        farm?.Name.Should().Be(farmDto.Name)
            .And.NotBeNull();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNothing_WhenFarmDoesNotExists()
    {
        _identityServiceMock.Setup(x => x.GetUserIdentity())
            .Returns(string.Empty);
        _farmServiceMock.Setup(x => x.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(() => null);

        var farm = await _sut.GetByNameAsync(Guid.NewGuid().ToString());

        farm.Should().BeNull();
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnFarm_WhenFarmExists()
    {
        var farmDto = _fixture.Create<FarmDto>();
        _farmServiceMock.Setup(x => x.GetByNameAsync(farmDto.Name!))
            .ReturnsAsync(farmDto);

        var farm = await _sut.GetByNameAsync(farmDto.Name!);

        farm?.Name.Should().Be(farmDto.Name)
            .And.NotBeNull();
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnNothing_WhenFarmDoesNotExists()
    {
        _farmServiceMock.Setup(x => x.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(() => null);

        var farm = await _sut.GetByNameAsync(Guid.NewGuid().ToString());

        farm.Should().BeNull();
    }

    [Fact]
    public async Task GetFarmStatistic()
    {
        var userId = _fixture.Create<string>();
        var farmStatistic = _fixture.Create<FarmStatisticDto>();
        var friendsFarms = _fixture.CreateMany<FarmDto>(1).ToList();
        _identityServiceMock.Setup(x => x.GetUserIdentity())
            .Returns(userId);
        _farmServiceMock.Setup(x => x.GetFarmStatistic(Guid.Parse(userId)))
            .ReturnsAsync(farmStatistic);

        var result = await _sut.GetFarmStatistic();

        result.Should().NotBeNull();
        result!.AverageAge.Should().Be(farmStatistic.AverageAge);
    }

    [Fact]
    public async Task CreateFarmAsync()
    {
        var userId = _fixture.Create<string>();
        var farmDto = _fixture.Create<FarmDto>();
        var okResult = new OkResult();
        _identityServiceMock.Setup(x => x.GetUserIdentity())
            .Returns(userId);
        _farmServiceMock.Setup(x => x.CreateAsync(Guid.Parse(userId), farmDto))
            .ReturnsAsync(okResult);

        await _sut.CreateAsync(farmDto);

        _farmServiceMock.Verify(x => x.CreateAsync(Guid.Parse(userId), farmDto), Times.Once);
    }

    [Fact]
    public async Task UpdateFarmAsync()
    {
        var userId = _fixture.Create<string>();
        var farmDto = _fixture.Create<FarmDto>();
        var okResult = new OkResult();
        _identityServiceMock.Setup(x => x.GetUserIdentity())
            .Returns(userId);
        _farmServiceMock.Setup(x => x.UpdateAsync(Guid.Parse(userId), farmDto))
            .ReturnsAsync(okResult);

        await _sut.UpdateAsync(farmDto);

        _farmServiceMock.Verify(x => x.UpdateAsync(Guid.Parse(userId), farmDto), Times.Once);
    }

    [Fact]
    public async Task DeleteFarmAsync()
    {
        var userId = _fixture.Create<string>();
        var okResult = new OkResult();
        _identityServiceMock.Setup(x => x.GetUserIdentity())
            .Returns(userId);
        _farmServiceMock.Setup(x => x.DeleteAsync(Guid.Parse(userId)))
            .ReturnsAsync(okResult);

        await _sut.DeleteAsync();

        _farmServiceMock.Verify(x => x.DeleteAsync(Guid.Parse(userId)), Times.Once);
    }
}
