[CmdletBinding()]
param (
)

$InformationPreference = 'Continue'
$aocYear = 2023
$sessionToken = $env:AOC_SESSION
$inputFilename = "input.txt"
$testInputFilename = "my-input.txt"
$inputCacheDirName = "input-cache"
$useCache = $false

if (-not $sessionToken) {
    Write-Error "AOC_SESSION environment variable is not set. Please set it to your session token."
    exit 1
}

$originalDirectory = Get-Location

# Populate directory from cache directory

if (Test-Path $inputCacheDirName) {
    $useCache = $true
    Write-Information "Cache directory exists, copying files to day directories"
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
            Copy-Item "$cacheDayDirectory/*" $destinationDirectory
            Write-Information "  Cached input '$cacheDayDirectoryName' copied to day directory '$destinationDirectoryName'"
        }
        catch {
            Write-Warning "Error while copying input for '$cacheDayDirectoryName'"
        }
    }
    Write-Information "  Finished cache extraction"
    Set-Location $originalDirectory
}
else {
    Write-Verbose "Cache directory not found"
}

$dayDirectories = Get-ChildItem -Directory | Where-Object { $_.Name -match "Day\d{2}" }

foreach ($dayDirectory in $dayDirectories) {
    try {
        Set-Location $dayDirectory
        $dayNumberString = $dayDirectory.Name -replace "Day(\d\d).*", '$1'
        $dayNumber = [int]$dayNumberString
        Write-Information "Restoring Day $dayNumber input"

        if (Test-Path $inputFilename) {
            $fileHash = (Get-FileHash $inputFilename -Algorithm MD5).Hash
            Write-Information "  Input file already exists, hash: $fileHash"
        }
        else {
            $url = "https://adventofcode.com/$aocYear/day/$dayNumber/input"
            Write-Information "  Downloading input file from $url"
            Invoke-WebRequest -Uri $url -Headers @{ Cookie = "session=$sessionToken" } -OutFile $inputFilename
            $fileHash = (Get-FileHash $inputFilename -Algorithm MD5).Hash
            Write-Information "  Input file downloaded, hash: $fileHash"
        }

        $testsInputDirectoryPath = "../Tests/Inputs/Day$dayNumberString"
        if (-not (Test-Path $testsInputDirectoryPath)) {
            $testsInputCreatedDirectory = New-Item -ItemType Directory -Path $testsInputDirectoryPath
            $testsInputRelativePath = Resolve-Path -Path $testsInputCreatedDirectory -Relative -RelativeBasePath $originalDirectory
            Write-Information "  Tests directory was not found, created at $testsInputRelativePath"
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
            Copy-Item $inputFilename $testsInputPath
            Write-Information "  Copied day input file to test inputs directory"
        }

        # Copy back to cache directory as some missing inputs may have been downloaded
        if ($useCache) {
            $cacheDayDirectory = Join-Path ".." $inputCacheDirName "day$dayNumberString"
            if (-not (Test-Path $cacheDayDirectory)) {
                $cacheDayCreatedDirectory = New-Item -ItemType Directory -Path $cacheDayDirectory
                $cacheDayRelativePath = Resolve-Path -Path $cacheDayCreatedDirectory -Relative -RelativeBasePath $originalDirectory
                Write-Information "  Cache directory not found, created at $cacheDayRelativePath"
            }
            Copy-Item $inputFilename $cacheDayDirectory
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
