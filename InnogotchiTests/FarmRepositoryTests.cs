using AutoMapper;
using DataAccessLayer.Data;
using DataAccessLayer.Repository;
using Entities.Entity;
using Microsoft.EntityFrameworkCore;
using Models.Core;

namespace InnogotchiTests;

public class FarmRepositoryTests
{
    private readonly FarmRepository _sut;
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly ApplicationContext _context;
    private Fixture _fixture;

    public FarmRepositoryTests()
    {
        var builder = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new ApplicationContext(builder.Options);
        var farmDto = new FarmDto() { Name = "Foo" };
        _mapperMock.Setup(x => x.Map<FarmDto>(It.IsAny<Farm>()))
            .Returns(farmDto);
        _sut = new(_mapperMock.Object, _context);
        _fixture = new Fixture();
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        _context.Farms!.AddRange(new Farm
        {
            Id = Guid.NewGuid(),
            Name= "Foo"
        });
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnNull_WhenFarmsDbIsEmpty()
    {
        var name = "Foo";

        var result = await _sut.GetByNameAsync(name);

        result.Should().NotBeNull();
    }
}
