namespace BaseLibrary.Tests.Unit;

public class LongRunnigTests
{
	[Fact(Timeout = 2000)]
	public async Task SlowTests()
	{
		await Task.Delay(10000);
	}
}