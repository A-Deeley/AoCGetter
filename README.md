#AoC Getter

## How to use

1. Add the necessary configuration to your `AppSettings.json` configuration under the `AdventOfCode` key.
2. Call `builder.Services.AddAoCGetter` and pass in the builder's `IConfiguration` object.
3. Request the `Puzzle` HttpClient in your service or class via constructor dependency injection.

## Examples

Adding the typed `Puzzle` http client:  
```
var configuration = builder.Configuration;

builder.Services.AddAoCGetter(configuraton);
```

Using the typed http client:  
```
public MyClass 
{
	readonly Puzzle _puzzles;

	public MyClass(Puzzle puzzles)
	{
		_puzzles = puzzles;
	}

	public void Day1Part1()
	{
		string[] puzzle = GetInput(1);
		// Puzzle logic here...
	}
}