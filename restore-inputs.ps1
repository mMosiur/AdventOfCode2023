[CmdletBinding()]
param (
    [Parameter(HelpMessage = "Use cached input files when available")]
    [switch]$UseCache
)

$InformationPreference = 'Continue'
$scriptName = $MyInvocation.MyCommand.Name
$aocYear = 2023
$branchName = (git branch --show-current)
if (-not $branchName)
{
    $branchName = "main"
}
$scriptWebAddress = "https://github.com/mMosiur/AdventOfCode$aocYear/blob/$branchName/$scriptName"
$author = "mpmorus@gmail.com"
$sessionToken = $env:AOC_SESSION
$inputFilename = "input.txt"
$testInputFilename = "my-input.txt"
$inputCacheDirName = "input-cache"

if (-not $sessionToken) {
    Write-Error "AOC_SESSION environment variable is not set. Please set it to your session token."
    exit 1
}

$originalDirectory = Get-Location

# Populate directory from cache directory

if ($UseCache) {
    if (Test-Path $inputCacheDirName) {
        Write-Information "Copying cache files to day directories"
        Set-Location $inputCacheDirName
        foreach ($cacheDayDirectory in Get-ChildItem -Directory) {
            $cacheDayDirectoryName = $cacheDayDirectory.Name
            try {
                $destinationDirectory = get-item "../$cacheDayDirectoryName*"
                if (-not $destinationDirectory) {
                    Write-Warning "Cache contains directory '$cacheDayDirectoryName' but no corresponding day directory was found."
                    continue
                }
                $destinationDirectoryName = $destinationDirectory.Name
                Copy-Item "$cacheDayDirectory/*" $destinationDirectory -Force
                Write-Information "  Cached input '$cacheDayDirectoryName' copied to day directory '$destinationDirectoryName'"
            }
            catch {
                Write-Warning "Error while copying input for '$cacheDayDirectoryName'"
            }
        }
        Set-Location $originalDirectory
    }
    else {
        $inputCacheCreatedDirectory = New-Item -ItemType Directory -Path $inputCacheDirName
        $inputCacheRelativePath = Resolve-Path -Path $inputCacheCreatedDirectory -Relative -RelativeBasePath $originalDirectory
        Write-Information "Cache directory not found, created at $inputCacheRelativePath"
    }
}

$dayDirectories = Get-ChildItem -Directory | Where-Object { $_.Name -match "Day\d{2}" }

$totalDaysCount = $dayDirectories.Count
$currentDayIndex = 0
foreach ($dayDirectory in $dayDirectories) {
    try {
        Set-Location $dayDirectory
        $dayNumberString = $dayDirectory.Name -replace "Day(\d\d).*", '$1'
        $dayNumber = [int]$dayNumberString
        $currentDayIndex++
        $percentComplete = ($currentDayIndex / $totalDaysCount) * 100
        Write-Progress -Activity "Restoring input files" -Status "Day $dayNumberString ($currentDayIndex/$totalDaysCount)" -PercentComplete $percentComplete
        Write-Information "Restoring Day $dayNumber input"

        if (Test-Path $inputFilename) {
            $fileHash = (Get-FileHash $inputFilename -Algorithm MD5).Hash
            Write-Information "  Input file already exists, hash: $fileHash"
        }
        else {
            $url = "https://adventofcode.com/$aocYear/day/$dayNumber/input"
            Write-Information "  Downloading input file from $url"
            Invoke-WebRequest -Uri $url -OutFile $inputFilename -Headers @{
                "Cookie" = "session=$sessionToken"
                "User-Agent" = "Mozilla/5.0 ($scriptWebAddress by $author)"
            }
            Set-ItemProperty -Path $inputFilename -Name IsReadOnly -Value $true
            $fileHash = (Get-FileHash $inputFilename -Algorithm MD5).Hash
            Write-Information "  Input file downloaded, hash: $fileHash"
        }

        $testsInputDirectoryPath = "../Tests/Inputs/Day$dayNumberString"
        if (-not (Test-Path $testsInputDirectoryPath)) {
            $testsInputCreatedDirectory = New-Item -ItemType Directory -Path $testsInputDirectoryPath
            $testsInputRelativePath = Resolve-Path -Path $testsInputCreatedDirectory -Relative -RelativeBasePath $originalDirectory
            Write-Information "  Tests directory was not found, created at $testsInputRelativePath"
        }
        else {
            # If directory existed, copy back potential example input files in them
            $exampleInputFiles = Get-ChildItem -Path $testsInputDirectoryPath -Filter "example-input*.txt"
            foreach ($exampleInputFile in $exampleInputFiles) {
                Set-ItemProperty -Path $exampleInputFile -Name IsReadOnly -Value $true
                $newExampleInputFileName = $exampleInputFile.Name -replace "^example-input-?", "example"
                Copy-Item -Path $exampleInputFile.FullName -Destination $newExampleInputFileName -Force
                Set-ItemProperty -Path $newExampleInputFileName -Name IsReadOnly -Value $true
            }
        }
        $testsInputPath = Join-Path $testsInputDirectoryPath $testInputFilename
        if (Test-Path $testsInputPath) {
            $testFileHash = (Get-FileHash $testsInputPath -Algorithm MD5).Hash
            if ($testFileHash -eq $fileHash) {
                Write-Verbose "  Test input file already exists, same hash"
            } else {
                Write-Warning "Test input file already exists but has a different hash than day directory file ($testFileHash)"
            }
        }
        else {
            Copy-Item -Path $inputFilename -Destination $testsInputPath -Force
            Set-ItemProperty -Path $testsInputPath -Name IsReadOnly -Value $true
            Write-Information "  Copied day input file to test inputs directory"
        }

        # Copy back to cache directory as some missing inputs may have been downloaded
        if ($UseCache)
        {
            $cacheDayDirectory = Join-Path ".." $inputCacheDirName "day$dayNumberString"
            if (-not (Test-Path $cacheDayDirectory)) {
                $cacheDayCreatedDirectory = New-Item -ItemType Directory -Path $cacheDayDirectory
                $cacheDayRelativePath = Resolve-Path -Path $cacheDayCreatedDirectory -Relative -RelativeBasePath $originalDirectory
                Write-Information "  Cache directory not found, created at $cacheDayRelativePath"
            }
            Copy-Item $inputFilename $cacheDayDirectory -Force
            Write-Information "  Copied day input file back to cache directory"
        }
        else {
            Write-Verbose "  Cache was not used, skipping copying back"
        }
    }
    finally {
        Set-Location $originalDirectory
    }
}
