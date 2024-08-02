using BaseApi.Models;
using BaseApi.Repositories;
using BaseApi.Services;
using FluentAssertions;
using NSubstitute;

namespace BaseApi.Tests.Unit;

public class UserServiceTests
{
    private readonly IUserService _sut;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    public UserServiceTests()
    {
        _sut = new UserService(_userRepository);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoUsersExists()
    {
        // Arrange
        _userRepository.GetAllAsync().Returns([]);

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
        users.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAListOfUsers_WhenUsersExists()
    {
        // Arrange
        var expectedUsers = new[] {
            new User { UserId = Guid.NewGuid(), FullName = "John Doe" },
            new User { UserId = Guid.NewGuid(), FullName = "Jane Doe" }
        };
        _userRepository.GetAllAsync().Returns(expectedUsers);

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
        users.Value.Should().BeEquivalentTo(expectedUsers);
    }
}