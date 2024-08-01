namespace BaseLibrary;

public class ValueSamples
{
	public string FullName = "Gustavo Rabanal";
	public int Age = 20;
	public DateOnly DateOfBirth = new(2004, 10, 10);

	public User AppUser = new()
	{
		FirstName = "Gustavo",
		Age = 20,
		DateOfBirth = new(2004, 10, 10)
	};

	public IEnumerable<User> Users =
    [
        new() { FirstName = "Gustavo", Age = 20, DateOfBirth = new(2004, 10, 10) },
		new() { FirstName = "Jacqueline", Age = 30, DateOfBirth = new(1994, 10, 10) },
		new() { FirstName = "Mildre", Age = 25, DateOfBirth = new(1999, 10, 10) },
		new() { FirstName = "Karen", Age = 40, DateOfBirth = new(1984, 10, 10) }
	];

	public IEnumerable<int> Numbers = [1, 2, 3, 4, 5];

	public event EventHandler EventHandler = default!;
	public virtual void RaiseEvent()
	{
		EventHandler(this, EventArgs.Empty);
	}
	internal int InternalValue = 10;
}
