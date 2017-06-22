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
#$branch = $env:APPVEYOR_REPO_BRANCH
$branch = 'development'

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
function Find-Assembly ([string] $assemblyVersionName) {
  $pattern = '\[assembly: {0}\("(.*)"\)\]' -f $assemblyVersionName
    (Get-Content $path) | ForEach-Object {
      if($_ -match $pattern) {
        return $matches[1]
      }
    }
}

[version]$fileVersion = Find-Assembly("AssemblyFileVersion")
[version]$localVersion = Find-Assembly("AssemblyInformationalVersion")

# TODO add check that verifies that AssemblyFIleVersion and AssemblyINformationalVersion contains same version.

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
Write-Host "Online version: $onlineVersion, local version: $localVersion"
[version] $onlineVersion = $onlineVersion
# This fails since $localVersion is 3.0.0.-1 while $fileVersion is 3.0.0.0
if ($localVersion -gt $onlineVersion) {
  Write-Host 'Local version is higher than online version, procceeding with deployment'
    if ($localVersion.Major -gt $onlineVersion.Major) {
      if ($localVersion.Minor -eq 0) {
        if ($localVersion.Build -eq 0) {
          Write-Host 'Deploying major build'
          Deploy $preRelease
          exit
        } else {
          throw "Invalid format for Major build, Patch($($localVersion.Build)) need to be set to 0"
        }
      } else {
        throw "Invalid format for Major build, Minor($($localVersion.Minor)) need to be set to 0"
      }
    }

  if ($localVersion.Minor -gt $onlineVersion.Minor) {
    Write-Host 'Validating versioning format'
      if ($localVersion.Build -eq 0) {
        Write-Host 'Validation Successfull!, deploying minor build'
        deploy $preRelease
        exit
      } else {
        throw "Invalid format for minor build, patch($($localVersion.Build)) need to be set to 0"
      }
  }

  if ($localVersion.Build -gt $onlineVersion.Build) {
    Write-Host 'Deploying patch build'
    deploy $preRelease
    exit
  }

} else {
  Write-Host 'Local version is not greater than online version, no deployment needed'
}
