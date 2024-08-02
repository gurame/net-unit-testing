using Ardalis.Result;
using AutoBogus;
using BaseApi.Contracts;
using BaseApi.Logging;
using BaseApi.Models;
using BaseApi.Repositories;
using BaseApi.Services;
using FluentAssertions;
using Mapster;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BaseApi.Tests.Unit;

public class UserServiceTests
{
    private readonly IUserService _sut;
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly ILoggerAdapter<UserService> _logger = Substitute.For<ILoggerAdapter<UserService>>();
    public UserServiceTests()
    {
        _sut = new UserService(_userRepository, _logger);
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
    public async Task GetAllAsync_ShouldReturnUsers_WhenSomeUsersExists()
    {
        // Arrange
        var expectedUsers = AutoFaker.Generate<User>(3);
        _userRepository.GetAllAsync().Returns(expectedUsers);

        // Act
        var users = await _sut.GetAllAsync();

        // Assert
        users.Value.Should().BeEquivalentTo(expectedUsers);
    }

    [Fact]
    public async Task GetAllAsync_ShouldLogMessages_WhenInvoked()
    {
        // Arrange
        _userRepository.GetAllAsync().Returns([]);

        // Act
        await _sut.GetAllAsync();

        // Assert
        _logger.Received(1).LogInformation(Arg.Is("Getting all users"));
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = AutoFaker.Generate<User>();
        var userResponse = user.Adapt<UserResponse>();
        _userRepository.GetByIdAsync(user.UserId).Returns(user);

        // Act
        var result = await _sut.GetByIdAsync(user.UserId);

        // Assert
        result.Value.Should().BeEquivalentTo(userResponse);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepository.GetByIdAsync(userId).ReturnsNull();

        // Act
        var user = await _sut.GetByIdAsync(userId);

        // Assert
        user.Status.Should().Be(ResultStatus.NotFound);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnUser_WhenUserIsCreated()
    {
        // Arrange
        var user = AutoFaker.Generate<User>();
        var userResponse = user.Adapt<UserResponse>();
        _userRepository.CreateAsync(user).Returns(true);
        _userRepository.GetByIdAsync(user.UserId).Returns(user);

        // Act
        var result = await _sut.CreateAsync(user);

        // Assert
        result.Value.Should().BeEquivalentTo(userResponse);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenUserIsNotCreated()
    {
        // Arrange
        var user = AutoFaker.Generate<User>();
        _userRepository.CreateAsync(user).Returns(false);

        // Act
        var result = await _sut.CreateAsync(user);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnUser_WhenUserIsUpdated()
    {
        // Arrange
        var user = AutoFaker.Generate<User>();
        var userResponse = user.Adapt<UserResponse>();
        _userRepository.UpdateAsync(user.UserId, user).Returns(true);
        _userRepository.GetByIdAsync(user.UserId).Returns(user);

        // Act
        var result = await _sut.UpdateAsync(user.UserId, user);

        // Assert
        result.Value.Should().BeEquivalentTo(userResponse);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnError_WhenUserIsNotUpdated()
    {
        // Arrange
        var user = AutoFaker.Generate<User>();
        _userRepository.GetByIdAsync(user.UserId).Returns(user);
        _userRepository.UpdateAsync(user.UserId, user).Returns(false);

        // Act
        var result = await _sut.UpdateAsync(user.UserId, user);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var user = AutoFaker.Generate<User>();
        _userRepository.GetByIdAsync(user.UserId).ReturnsNull();

        // Act
        var result = await _sut.UpdateAsync(user.UserId, user);

        // Assert
        result.Status.Should().Be(ResultStatus.NotFound);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnNoContent_WhenUserIsDeleted()
    {
        // Arrange
        var user = AutoFaker.Generate<User>();
        _userRepository.GetByIdAsync(user.UserId).Returns(user);
        _userRepository.DeleteAsync(user.UserId).Returns(true);

        // Act
        var result = await _sut.DeleteAsync(user.UserId);

        // Assert
        result.Status.Should().Be(ResultStatus.NoContent);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnError_WhenUserIsNotDeleted()
    {
        // Arrange
        var user = AutoFaker.Generate<User>();
        _userRepository.GetByIdAsync(user.UserId).Returns(user);
        _userRepository.DeleteAsync(user.UserId).Returns(false);

        // Act
        var result = await _sut.DeleteAsync(user.UserId);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepository.GetByIdAsync(userId).ReturnsNull();

        // Act
        var result = await _sut.DeleteAsync(userId);

        // Assert
        result.Status.Should().Be(ResultStatus.NotFound);
    }   
}