# Documentation

## Overview

This project is a .NET solution that includes multiple projects and unit tests. The structure of the project is as follows:

- [`src/`]: Contains the main source code for the application.
- [`tests/`]: Contains unit tests for the application.
- [`Makefile`]: Provides commands to build, test, and run the project.

## Unit Testing

The unit tests are organized into two main projects:

- `BaseApi.Tests.Unit`: Contains unit tests for the `BaseApi` project.
- `BaseLibrary.Tests.Unit`: Contains unit tests for the `BaseLibrary` project.

### Mocking with NSubstitute

NSubstitute is used for creating mock objects in the unit tests. This allows us to simulate the behavior of dependencies and verify interactions.

Example from [`UserServiceTests`]:
```csharp
using NSubstitute;

public class UserServiceTests
{
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly ILogger<UserService> _logger = Substitute.For<ILogger<UserService>>();
    private readonly UserService _sut;

    public UserServiceTests()
    {
        _sut = new UserService(_userRepository, _logger);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenUserIsNotCreated()
    {
        // Arrange
        _userRepository.CreateAsync(Arg.Any<User>()).ReturnsNull();

        // Act
        var result = await _sut.CreateAsync(new User());

        // Assert
        result.Should().BeOfType<Result<User>>().Which.Status.Should().Be(ResultStatus.Error);
    }
}
```

### Assertions with FluentAssertions

FluentAssertions is used for writing assertions in a more readable and fluent manner.

Example from `UserServiceTests`:
```csharp
using FluentAssertions;

[Fact]
public async Task CreateAsync_ShouldReturnUser_WhenUserIsCreated()
{
    // Arrange
    var user = new User { Id = 1, Name = "Test User" };
    _userRepository.CreateAsync(Arg.Any<User>()).Returns(user);

    // Act
    var result = await _sut.CreateAsync(user);

    // Assert
    result.Should().BeOfType<Result<User>>().Which.Value.Should().Be(user);
}
```

## Running Tasks with Makefile

The [`Makefile`] provides several commands to build, test, and run the project. Here are some of the main commands:

- **build**: Builds the entire solution.
  ```sh
  make build
  ```
  This command runs `dotnet build UnitTesting.sln`.

- **test**: Runs all the tests in the solution.
  ```sh
  make test
  ```
  This command runs `dotnet test UnitTesting.sln`.

- **test-coverage-generate**: Generates a test coverage report for the `BaseApi.Tests.Unit` project.
  ```sh
  make test-coverage-generate
  ```
  This command runs:
  ```sh
  dotnet test ./tests/BaseApi.Tests.Unit/BaseApi.Tests.Unit.csproj \
      /p:CollectCoverage=true \
      /p:CoverletOutputFormat=cobertura \
      /p:Exclude="[BaseApi]BaseApi.Data.*%2C[BaseApi]BaseApi.Repositories.*%2C[BaseApi]BaseApi.Endpoints.*%2C[BaseApi]BaseApi.Logging.*"
  ```

- **test-coverage-report**: Generates an HTML report from the coverage data and opens it in the default browser.
  ```sh
  make test-coverage-report
  ```
  This command runs:
  ```sh
  reportgenerator "-reports:./tests/BaseApi.Tests.Unit/coverage.cobertura.xml" \
      "-targetdir:./tests/BaseApi.Tests.Unit/coverage-report" -reporttypes:Html
  $(OPEN_CMD) ./tests/BaseApi.Tests.Unit/coverage-report/index.html
  ```

  **Note**: Before running the `test-coverage-report` task, you need to install the `reportgenerator` tool:
  ```sh
  dotnet tool install -g dotnet-reportgenerator-globaltool
  ```

- **run-api**: Runs the `BaseApi` project.
  ```sh
  make run-api
  ```
  This command runs `dotnet run --project ./src/BaseApi/BaseApi.csproj`.

## Conclusion

This documentation provides an overview of the project structure, the `Makefile` commands, and the unit tests. For more detailed information, refer to the individual files and classes in the project.