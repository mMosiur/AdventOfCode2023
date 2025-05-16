namespace AdventOfCode.Year2023.Day10.Puzzle.Pipes;

[Flags]
internal enum PipeTile
{
    Ground = 0,

    LeadsUp = 1 << 0,
    LeadsRight = 1 << 1,
    LeadsDown = 1 << 2,
    LeadsLeft = 1 << 3,

    Vertical = LeadsUp | LeadsDown,
    Horizontal = LeadsRight | LeadsLeft,
    NorthEast = LeadsUp | LeadsRight,
    NorthWest = LeadsUp | LeadsLeft,
    SouthWest = LeadsDown | LeadsLeft,
    SouthEast = LeadsDown | LeadsRight,

    StartingPosition = LeadsUp | LeadsRight | LeadsDown | LeadsLeft,
}
