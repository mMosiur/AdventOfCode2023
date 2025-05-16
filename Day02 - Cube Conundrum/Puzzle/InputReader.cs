using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Day02.Puzzle;

internal static class InputReader
{
	private static readonly Regex LineRegex = new(@"Game (\d+):\s*(.*)\s*", RegexOptions.Compiled);
	private static readonly Regex CubeColorRegex = new(@"(\d+) (\w+)", RegexOptions.Compiled);

	public static Game ParseGame(string line)
	{
		var match = LineRegex.Match(line);
		if (!match.Success)
		{
			throw new InputException($"Input line '{line}' is not in the expected format.");
		}

		int gameId = int.Parse(match.Groups[1].ValueSpan);

		var cubeSets = match.Groups[2].Value
			.Split(';', StringSplitOptions.TrimEntries)
			.Select(ParseCubeSet)
			.ToList();

		return new Game
		{
			Id = gameId,
			Cubes = cubeSets
		};
	}

	private static CubeSet ParseCubeSet(string s)
	{
		string[] cubeStrings = s.Split(',', StringSplitOptions.TrimEntries);
		int red = 0;
		int green = 0;
		int blue = 0;
		foreach (string cubeString in cubeStrings)
		{
			var match = CubeColorRegex.Match(cubeString);
			if (!match.Success)
			{
				throw new InputException($"Cube color '{cubeString}' is not in the expected format.");
			}

			int count = int.Parse(match.Groups[1].ValueSpan);
			var color = match.Groups[2].ValueSpan;
			switch (color)
			{
				case "red":
					red += count;
					break;
				case "green":
					green += count;
					break;
				case "blue":
					blue += count;
					break;
				default:
					throw new InputException($"Cube color '{color}' is not supported.");
			}
		}

		return new CubeSet
		{
			Red = red,
			Green = green,
			Blue = blue
		};
	}
}
