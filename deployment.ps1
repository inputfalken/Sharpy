$packageSource = 'https://www.nuget.org/api/v2/package'

function Deploy ([bool] $alpha) {
  if ($alpha) {
    nuget pack .\Sharpy\Sharpy.csproj  -IncludeReferencedProjects -Prop configuration=release -Suffix alpha
    nuget push ".\Sharpy.$($fileVersion.Major).$($fileVersion.Minor).$($fileVersion.Build)-alpha.nupkg" -Verbosity detailed -ApiKey $env:NUGET_API_KEY -Source $packageSource
  } else {
    nuget pack .\Sharpy\Sharpy.csproj  -IncludeReferencedProjects -Prop configuration=release
    nuget push ".\Sharpy.$($fileVersion.Major).$($fileVersion.Minor).$($fileVersion.Build).nupkg" -Verbosity detailed -ApiKey $env:NUGET_API_KEY -Source $packageSource
  }
}
# For Appveyro
$branch = $env:APPVEYOR_REPO_BRANCH

# Determine if it's Pre release
switch ($branch) {
  "development" {
    Write-Host "Proceeding script with alpha version for branch: $branch."
    $preRelease = 1
  }
  "master" {
    Write-Host "Proceeding script with stable version for branch: $branch."
    $preRelease = 0
  }
  default {
    Write-Host "$branch is not a deployable branch exiting..."
    exit
  }
}

# Find current assembly info
#$path = (Get-Item -Path ".\" -Verbose).FullName + "\Sharpy\Properties\Assemblyinfo.cs"
$path = '.\Sharpy\Properties\AssemblyInfo.cs'
function Update-Assembly ([string] $assemblyVersionName) {
  $pattern = '\[assembly: {0}\("(.*)"\)\]' -f $assemblyVersionName
    (Get-Content $path) | ForEach-Object {
      if($_ -match $pattern) {
        return [version]$matches[1]
      }
    }
}

$assemblyFileVersion = "AssemblyFileVersion"
$assemblyInformationVersion = "AssemblyInformationalVersion"
[version]$fileVersion = Update-Assembly($assemblyFileVersion)
[version]$informationalVersion = Update-Assembly($assemblyInformationVersion)

# Checks that they both contain same semver
# StartsWith is needed because AssemblyFileVersion uses a different format.
  if (!$assemblyFileVersion.ToString().StartsWith($assemblyInformationVersion).ToString()) {
    throw "Assembly attributes $assemblyFileVersion and $assemblyInformationVersion are missmatched in ./Sharpy/Properties/Assemblyinfo.cs   "
  }

$listSource = 'https://nuget.org/api/v2/'
Write-Host "Fetching nuget version from $listSource"
if ($preRelease) {
  $nug = NuGet list Sharpy -PreRelease -Source $listSource
} else {
  $nug = NuGet list Sharpy -Source $listSource
}
# Select last element of split on " "
$onlineVersion = ($nug.Split(" ") | Select-Object -Last 1)

# Remove string "alpha" from last element of $onlineVersion if it's pre release
if ($preRelease) {
  $onlineVersion = ($onlineVersion | select -Last 1).Split("-") | select -First 1
}
Write-Host "Online version: $onlineVersion, local version: $fileVersion"
[version] $onlineVersion = $onlineVersion

if ($fileVersion -gt $onlineVersion) {
  Write-Host 'Local version is higher than online version, procceeding with deployment'
    if ($fileVersion.Major -gt $onlineVersion.Major) {
      if ($fileVersion.Minor -eq 0) {
        if ($fileVersion.Build -eq 0) {
          Write-Host 'Deploying major build'
          Deploy $preRelease
          exit
        } else {
          throw "Invalid format for Major build, Patch($($fileVersion.Build)) need to be set to 0"
        }
      } else {
        throw "Invalid format for Major build, Minor($($fileVersion.Minor)) need to be set to 0"
      }
    }

  if ($fileVersion.Minor -gt $onlineVersion.Minor) {
    Write-Host 'Validating versioning format'
      if ($fileVersion.Build -eq 0) {
        Write-Host 'Validation Successfull!, deploying minor build'
        deploy $preRelease
        exit
      } else {
        throw "Invalid format for minor build, patch($($fileVersion.Build)) need to be set to 0"
      }
  }

  if ($fileVersion.Build -gt $onlineVersion.Build) {
    Write-Host 'Deploying patch build'
    deploy $preRelease
    exit
  }

} else {
  Write-Host 'Local version is lower than online version, no deployment needed'
}

