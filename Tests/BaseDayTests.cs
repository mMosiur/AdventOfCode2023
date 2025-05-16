using AdventOfCode.Common;

namespace AdventOfCode.Year2023.Tests;

/// <summary>
/// The base day tests. Allows subclasses to simply implement few needed methods and call the <c>BaseTestPart1</c> or <c>BaseTestPart2</c> methods
/// to universally test general part solving of the <see cref="DaySolver{TDaySolverOptions}"/>.
/// </summary>
/// <typeparam name="TDaySolver">The solver class for given day</typeparam>
/// <typeparam name="TDaySolverOptions">The solver options class that is being used by the given day</typeparam>
public abstract class BaseDayTests<TDaySolver, TDaySolverOptions>
	where TDaySolver : DaySolver<TDaySolverOptions>
	where TDaySolverOptions : DaySolverOptions, new()
{
	/// <summary>
	/// The base directory for input files.
	/// </summary>
	private const string BaseInputsDirectory = "Inputs";

	/// <summary>
	/// The name of the directory containing the input files for this day.
	/// </summary>
	/// <remarks>
	/// This directory will be in fact a subdirectory of a general <see cref="BaseInputsDirectory"/> constant.
	/// </remarks>
	protected abstract string DayInputsDirectory { get; }

	/// <summary>
	/// Creates the <typeparamref name="TDaySolver"/> day solver using given <typeparamref name="TDaySolverOptions"/> options.
	/// </summary>
	/// <remarks>
	/// <para>
	/// We can't specify parametrized constraint for <typeparamref name="TDaySolver"/> argument, so we have to use this workaround.
	/// Subclasses should override this property with simply "<c>return new <typeparamref name="TDaySolver"/>(options);</c>".
	/// </para>
	/// <para>
	/// Note that <see cref="DaySolverOptions.InputFilepath"/> property of the <paramref name="options"/> parameter
	/// <b>will</b> be overridden by the test fixture with <see cref="GetInputFilepath"/> return value.
	/// </para>
	/// </remarks>
	protected abstract TDaySolver CreateSolver(TDaySolverOptions options);

	/// <summary>
	/// Gets the input filepath for the current day.
	/// </summary>
	/// <remarks>
	/// Uses <see cref="BaseInputsDirectory"/> constant as the base directory and joins it with
	/// <see cref="DayInputsDirectory"/> property of the current test fixture (so the subdirectory with input files for this day)
	/// and the <paramref name="inputFilename"/> parameter.
	/// </remarks>
	/// <param name="inputFilename">The filename of the input file.</param>
	/// <returns>The whole file path of the input file</returns>
	private string GetInputFilepath(string inputFilename)
	{
		string filepath = Path.Combine(
			BaseInputsDirectory,
			DayInputsDirectory,
			inputFilename
		);
		bool testFileExists = File.Exists(filepath);
		Assert.True(testFileExists, $"Specified test input file does not exist: {filepath}");
		return filepath;
	}

	/// <summary>
	/// Tests the first part of the day.
	/// </summary>
	/// <remarks>
	/// Creates a <typeparamref name="TDaySolver"/> day solver using given <paramref name="options"/> (or default if not specified).
	/// Then calls <see cref="DaySolver{TDaySolverOptions}.SolvePart1"/> method of created solver and asserts that the result is equal to the <paramref name="expectedResult"/>.
	/// </remarks>
	/// <param name="inputFilename">The filename of the input file use during test</param>
	/// <param name="expectedResult">The expected return value for given test file</param>
	/// <param name="options">Options used to create the solver; uses default values if <c>null</c></param>
	protected void BaseTestPart1(string inputFilename, string expectedResult, TDaySolverOptions? options = null)
	{
		options ??= new();
		options.InputFilepath = GetInputFilepath(inputFilename);
		TDaySolver solver = CreateSolver(options);
		string result = solver.SolvePart1();
		Assert.Equal(expectedResult, result);
	}

	/// <summary>
	/// Tests the second part of the day.
	/// </summary>
	/// <remarks>
	/// Creates a <typeparamref name="TDaySolver"/> day solver using given <paramref name="options"/> (or default if not specified).
	/// Then calls <see cref="DaySolver{TDaySolverOptions}.SolvePart2"/> method of created solver and asserts that the result is equal to the <paramref name="expectedResult"/>.
	/// </remarks>
	/// <param name="inputFilename">The filename of the input file use during test</param>
	/// <param name="expectedResult">The expected return value for given test file</param>
	/// <param name="options">Options used to create the solver; uses default values if <c>null</c></param>
	protected void BaseTestPart2(string inputFilename, string expectedResult, TDaySolverOptions? options = null)
	{
		options ??= new();
		options.InputFilepath = GetInputFilepath(inputFilename);
		TDaySolver solver = CreateSolver(options);
		string result = solver.SolvePart2();
		Assert.Equal(expectedResult, result);
	}
}
