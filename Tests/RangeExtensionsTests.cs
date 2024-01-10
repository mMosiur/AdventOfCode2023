using AdventOfCode.Year2023.Day05.ExtendedMath;
using Range = AdventOfCode.Common.Numerics.Interval<uint>;

namespace AdventOfCode.Year2023.Tests;

public class RangeExtensionsTests
{
	[Fact]
	public void Remove_ReturnsOriginalRange_WhenOtherRangeDoesNotOverlap()
	{
		// Arrange
		var range = new Range(5, 10);
		var other = new Range(15, 20);

		// Act
		var result = range.Remove(other);

		// Assert
		Assert.Equal(range, result.Item1);
		Assert.Null(result.Item2);
	}

	[Fact]
	public void Remove_ReturnsNull_WhenOtherRangeCoversOriginalRange()
	{
		// Arrange
		var range = new Range(5, 10);
		var other = new Range(0, 15);

		// Act
		var result = range.Remove(other);

		// Assert
		Assert.Null(result.Item1);
		Assert.Null(result.Item2);
	}

	[Fact]
	public void Remove_ReturnsTwoRanges_WhenOtherRangeIsInsideOriginalRange()
	{
		// Arrange
		var range = new Range(5, 15);
		var other = new Range(10, 12);

		// Act
		var result = range.Remove(other);

		// Assert
		Assert.Equal(new Range(5, 9), result.Item1);
		Assert.Equal(new Range(13, 15), result.Item2);
	}

	[Fact]
	public void Remove_ReturnsOneRange_WhenOtherRangeOverlapsFromStart()
	{
		// Arrange
		var range = new Range(5, 15);
		var other = new Range(0, 10);

		// Act
		var result = range.Remove(other);

		// Assert
		Assert.Null(result.Item1);
		Assert.Equal(new Range(11, 15), result.Item2);
	}

	[Fact]
	public void Remove_ReturnsOneRange_WhenOtherRangeOverlapsToEnd()
	{
		// Arrange
		var range = new Range(5, 15);
		var other = new Range(10, 20);

		// Act
		var result = range.Remove(other);

		// Assert
		Assert.Equal(new Range(5, 9), result.Item1);
		Assert.Null(result.Item2);
	}
}
