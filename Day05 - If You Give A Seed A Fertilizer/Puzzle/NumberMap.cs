﻿using AdventOfCode.Year2023.Day05.ExtendedMath;

namespace AdventOfCode.Year2023.Day05.Puzzle;

internal sealed class NumberMap(NumberCategory sourceCategory, NumberCategory destinationCategory, IReadOnlyList<NumberMapLine> lines)
{
	public NumberCategory SourceCategory { get; } = sourceCategory;
	public NumberCategory DestinationCategory { get; } = destinationCategory;
	public IReadOnlyList<NumberMapLine> Lines { get; } = lines;

	public MultiRange ConvertRanges(MultiRange sourceRanges)
	{
		var destinationRanges = new MultiRange(sourceRanges.Count);
		var unmappedRanges = new MultiRange(sourceRanges);
		foreach (var line in Lines)
		{
			foreach (var range in sourceRanges)
			{
				if (!range.Overlaps(line.SourceRange)) continue;
				var intersection = range.Intersect(line.SourceRange);
				if (intersection.HasValue)
				{
					destinationRanges.Add(intersection.Value.MoveBy(line.DestinationOffset));
					unmappedRanges.Remove(intersection.Value);
				}
			}
		}

		destinationRanges.Add(unmappedRanges);

		return destinationRanges;
	}

	public uint ConvertNumber(uint sourceNumber)
	{
		var line = Lines.FirstOrDefault(l => l.SourceRange.Contains(sourceNumber));
		if (line == default)
		{
			return sourceNumber;
		}

		checked // TODO: remove checked section after testing
		{
			return (uint)(sourceNumber + line.DestinationOffset);
		}
	}

	public override string ToString()
	{
		return $"NumberMap {SourceCategory} -> {DestinationCategory} ({Lines.Count})";
	}
}
