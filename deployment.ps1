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
# AppVeyor Environmental varible
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
$path = '.\Sharpy\Properties\AssemblyInfo.cs'
function Find-Assembly ([string] $assemblyVersionName) {
  $pattern = '\[assembly: {0}\("(.*)"\)\]' -f $assemblyVersionName
    (Get-Content $path) | ForEach-Object {
      if($_ -match $pattern) {
        return $matches[1]
      }
    }
}

# TODO add check that verifies that AssemblyFIleVersion and AssemblyINformationalVersion contains same version.
# The version inside [assembly: AssemblyFileVersion(*.*.*.*)],
[version]$fileVersion = Find-Assembly("AssemblyFileVersion")
# Local Nuget semver
[version]$localVersion = Find-Assembly("AssemblyInformationalVersion")

# Source for NuGet to get the latest package online
$listSource = 'https://nuget.org/api/v2/'
Write-Host "Fetching nuget version from $listSource"

# Use alpha version if the current branch is development
if ($preRelease) {
  $nug = NuGet list Sharpy -PreRelease -Source $listSource
} else {
  $nug = NuGet list Sharpy -Source $listSource
}
# $nug comes in format: "packageName 1.0.0".
$onlineVersion = ($nug.Split(" ") | Select-Object -Last 1)

# In alpha version the version also includes the string "version-alpha" where version is the semver.
if ($preRelease) {
  $onlineVersion = ($onlineVersion | select -Last 1).Split("-") | select -First 1
}
# Online NuGet semver
[version] $onlineVersion = $onlineVersion

# Checks if deployment is needed by comparing local and online version
if ($localVersion -gt $onlineVersion) {
  Write-Host "Local version($localVersion) is higher than online version($onlineVersion), proceeding with deployment"
    if ($localVersion.Major -gt $onlineVersion.Major) {
      if ($localVersion.Minor -eq 0) {
        if ($localVersion.Build -eq 0) {
          Write-Host 'Validation Successfull!, deploying major build'
            Deploy $preRelease
        } else {
          throw "Invalid format for Major build, Patch($($localVersion.Build)) need to be set to 0"
        }
      } else {
        throw "Invalid format for Major build, Minor($($localVersion.Minor)) need to be set to 0"
      }
    }
    elseif ($localVersion.Minor -gt $onlineVersion.Minor) {
      Write-Host 'Validating versioning format'
      if ($localVersion.Build -eq 0) {
        Write-Host 'Validation Successfull!, deploying minor build'
        deploy $preRelease
      } else {
          throw "Invalid format for minor build, patch($($localVersion.Build)) need to be set to 0"
      }
    }
    elseif ($localVersion.Build -gt $onlineVersion.Build) {
      Write-Host 'Deploying patch build'
      deploy $preRelease
    }

} else {
  Write-Host 'Local version is not greater than online version, no deployment needed'
}
