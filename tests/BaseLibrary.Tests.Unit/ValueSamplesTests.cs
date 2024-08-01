using FluentAssertions;

namespace BaseLibrary.Tests.Unit;

public class ValueSamplesTests
{
	private readonly ValueSamples _sut = new();

	[Fact]
	public void FullName_ShouldBe_GustavoRabanal()
	{
		var fullName = _sut.FullName;

		fullName.Should().NotBeEmpty();
		fullName.Should().StartWith("Gustavo");
		fullName.Should().EndWith("Rabanal");
		fullName.Should().Be("Gustavo Rabanal");
	}

	[Fact]
	public void Age_ShouldBe_20()
	{
		var age = _sut.Age;

		age.Should().BePositive();
		age.Should().BeGreaterThan(18);
		age.Should().BeInRange(18, 30);
		age.Should().Be(20);
	}

	[Fact]
	public void DateOfBirth_ShouldBe_20041010()
	{
		var dateOfBirth = _sut.DateOfBirth;

		dateOfBirth.Year.Should().Be(2004);
		dateOfBirth.Month.Should().Be(10);
		dateOfBirth.Day.Should().Be(10);
		dateOfBirth.Should().BeAfter(new DateOnly(2000, 10, 9));
		dateOfBirth.Should().Be(new DateOnly(2004, 10, 10));
	}

	[Fact]
	public void AppUser_ShouldBe_Gustavo()
	{
		var expected = new User
		{
			FirstName = "Gustavo",
			Age = 20,
			DateOfBirth = new DateOnly(2004, 10, 10)
		};

		var user = _sut.AppUser;

		user.Should().BeEquivalentTo(expected);
	}

	[Fact]
	public void Users_ShouldHave_4Users()
	{
		var expected = new User
		{
			FirstName = "Gustavo",
			Age = 20,
			DateOfBirth = new DateOnly(2004, 10, 10)
		};

		var users = _sut.Users;

		users.Should().NotBeNullOrEmpty();
		users.Should().HaveCount(4);
		users.Should().ContainEquivalentOf(expected);
		users.Should().ContainSingle(u => u.FirstName == "Gustavo" && u.Age > 18);
		users.Should().Contain(u => u.FirstName == "Jacqueline");
		users.Should().Contain(u => u.FirstName == "Mildre");
		users.Should().Contain(u => u.FirstName == "Karen");
	}

	[Fact]
	public void Numbers_ShouldHave_5Numbers()
	{
		var numbers = _sut.Numbers;

		numbers.Should().NotBeNullOrEmpty();
		numbers.Should().HaveCount(5);
		numbers.Should().ContainInOrder(1, 2, 3, 4, 5);
		numbers.Should().ContainSingle(n => n > 4);
		numbers.Should().Contain(n => n % 2 == 0);
		numbers.Should().Contain(n => n % 2 != 0);
	}

	[Fact]
	public void ExceptionThrown_When_DivideByZero()
	{
		var calculator = new Calculator();

		Action act = () => calculator.Divide(10, 0);

		act.Should().Throw<DivideByZeroException>()
				.WithMessage("Attempted to divide by zero.");
	}

	[Fact]
	public void EventHandler_ShouldBe_Raised()
	{
		var monitor = _sut.Monitor();

		_sut.RaiseEvent();

		monitor.Should().Raise("EventHandler");
	}

	[Fact]
	public void InternalValue_ShouldBe_10()
	{
		var internalValue = _sut.InternalValue;
		
		internalValue.Should().BePositive();
		internalValue.Should().BeGreaterThan(5);
		internalValue.Should().Be(10);
	}	
}
