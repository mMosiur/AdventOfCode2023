[CmdletBinding()]
param(
    [Parameter(Mandatory = $true, HelpMessage = "In 'dayXX' or 'XX' format, later reformatted to 'DayXX'")]
    [string]$day,

    [Parameter(ValueFromRemainingArguments = $true)]
    [string]$dayTitle
)

$year = 2023

if ($day -notmatch '^(day)?(\d\d?)$')
{
    Write-Error "Invalid day number"
    exit 1
}
$day = [int]$Matches[2]
if ($day -lt 1 -or $day -gt 25)
{
    Write-Error "Invalid day number, available days are 1 to 25 (got $day)"
    exit 1
}
$day = '{0:d2}' -f [int]$day
$day = "Day$day"

if (-not $dayTitle)
{
    $providedTitle = Read-Host "Title (leave empty to skip)"
    if ($providedTitle)
    {
        $dayTitle = $providedTitle
    }
}

if (-not $dayTitle)
{
    dotnet new aocday --year $year -o "$day"
    dotnet sln add ".\$day\"
    cd Tests
    dotnet add reference "..\$day"
    cd ..
}
else
{
    dotnet new aocday --year $year -o "$day" --title "$dayTitle"
    $folderName = "$day - $dayTitle"
    Rename-Item "$day" "$folderName"
    dotnet sln add ".\$folderName\" --in-root
    cd Tests
    dotnet add reference "..\$folderName"
    cd ..
}
