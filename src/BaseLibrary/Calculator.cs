namespace BaseLibrary;

public class Calculator
{
	public int Add(int a, int b) => a + b;
	public int Subtract(int a, int b) => a - b;
	public int Multiply(int a, int b) => a * b;
	public float Divide(int a, int b)
	{
		EnsureThatDivisorIsNotZero(b);
		return (float)a / b;
	}
	private static void EnsureThatDivisorIsNotZero(int b)
	{
		if (b == 0)
		{
			throw new DivideByZeroException();
		}
	}
}
