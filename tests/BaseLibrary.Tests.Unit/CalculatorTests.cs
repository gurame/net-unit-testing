using FluentAssertions;
using Xunit.Abstractions;

namespace BaseLibrary.Tests.Unit;

public class CalculatorTests : IAsyncLifetime
{
    private readonly Calculator _sut = new();
    private readonly ITestOutputHelper _output;
    public CalculatorTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(1, 2, 3, Skip = "This test is skipped")] 
    [MemberData(nameof(AddTestData))]
    public void Add_ShouldAddTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
    {
        // Act
        var result = _sut.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [ClassData(typeof(CalculatorSubtractTestData))]
    public void Subtract_ShouldSubtractTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
    {
        // Act
        var result = _sut.Subtract(a, b);

        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(2, 3, 6)]
    [InlineData(3, 8, 24)]
    public void Multiply_ShouldMultiplyTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
    {
        // Act
        var result = _sut.Multiply(a, b);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(10, 5, 2)]
    [InlineData(16, 4, 4)]
    public void Divide_ShouldDivideTwoNumbers_WhenTwoNumbersAreIntegers(int a, int b, int expected)
    {
        // Act
        var result = _sut.Divide(a, b);

        // Assert
        result.Should().Be(expected);
    }

    public async Task InitializeAsync() 
    {
        _output.WriteLine("CalculatorTests started");
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        _output.WriteLine("CalculatorTests ended");
        await Task.CompletedTask;
    }

    public static IEnumerable<object[]> AddTestData =>
        [
            [1, 2, 3],
            [2, 3, 5]
        ];
}