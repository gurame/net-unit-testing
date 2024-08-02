using Xunit.Abstractions;

namespace BaseLibrary.Tests.Unit;

// The LibraryClassFixture is a shared fixture that will be used by all tests in the DefaultLibraryTests class
// The default behavior of xUnit is to create a new instance of the test class for each test method
// Paralellization is disabled by default in xUnit, so the tests will run sequentially
// Collections run in parallel, so the tests in the LibraryCollectionFixtureOne and LibraryCollectionFixtureTwo classes will run in parallel
public class DefaultLibraryTests : IClassFixture<LibraryClassFixture>
{
	private readonly LibraryClassFixture _fixture;
	private readonly ITestOutputHelper _output;
    public DefaultLibraryTests(LibraryClassFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
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