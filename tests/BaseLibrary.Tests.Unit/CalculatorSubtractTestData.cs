using System.Collections;

namespace BaseLibrary.Tests.Unit;

public class CalculatorSubtractTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 10, 5, 5 };
        yield return new object[] { 20, 10, 10 };
        yield return new object[] { 30, 15, 15 };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
