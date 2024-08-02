using Xunit.Abstractions;

namespace BaseLibrary.Tests.Unit;

[Collection("LibraryCollection")]
public class LibraryCollectionFixtureOne
{
	private readonly ITestOutputHelper _output;
	private readonly LibraryClassFixture _fixture;
    public LibraryCollectionFixtureOne(ITestOutputHelper output, LibraryClassFixture fixture)
    {
        _output = output;
        _fixture = fixture;
    }

    [Fact]
	public void Test1()
	{
		_output.WriteLine("Guid from LibraryClassFixture: {0}", _fixture.Id);
	}

	[Fact]
	public void Test2()
	{
		_output.WriteLine("Guid from LibraryClassFixture: {0}", _fixture.Id);
	}
}

[Collection("LibraryCollection")]
public class LibraryCollectionFixtureTwo
{
	private readonly ITestOutputHelper _output;
	private readonly LibraryClassFixture _fixture;
    public LibraryCollectionFixtureTwo(ITestOutputHelper output, LibraryClassFixture fixture)
    {
        _output = output;
        _fixture = fixture;
    }

    [Fact]
	public void Test1()
	{
		_output.WriteLine("Guid from LibraryClassFixture: {0}", _fixture.Id);
	}

	[Fact]
	public void Test2()
	{
		_output.WriteLine("Guid from LibraryClassFixture: {0}", _fixture.Id);
	}
}
