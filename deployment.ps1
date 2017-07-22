# Find assembly version info.
function Find-Assembly ([string] $assemblyVersionName) {
  $pattern = '\[assembly: {0}\("(.*)"\)\]' -f $assemblyVersionName
    (Get-Content '.\Sharpy\Properties\AssemblyInfo.cs') | ForEach-Object {
      if($_ -match $pattern) {
        return $matches[1]
      }
    }
}

# Delete old nuget package
function deleteNugetPackage ([string] $label, [string] $suffix, [bool] $suffixBuild) {
  if ($localVersion.Major -eq $onlineVersion.Major) {
    Write-Host "Same major build, deleting old package" -ForegroundColor yellow
    if ($suffixBuild) {
      nuget delete Sharpy $onlineVersion-$suffix -ApiKey $nugetApiKey -Source $packageSource -NonInteractive
    } else {
      nuget delete Sharpy $onlineVersion -ApiKey $nugetApiKey -Source $packageSource -NonInteractive
    }
  } else {
    Write-Host "New major build, ignoring package deletion" -ForegroundColor yellow
  }
}

# NuGet Deployment
function deployToNuget ([string] $label, [bool] $suffixBuild) {
  $fileName = ".\Sharpy.$($localVersion.Major).$($localVersion.Minor).$($localVersion.Build)"
  $suffix = 'alpha'
  $project = '.\Sharpy\Sharpy.csproj'

  if ($suffixBuild) {
    nuget pack $project -IncludeReferencedProjects -Prop configuration=release -Suffix $suffix
    nuget push "$($fileName)-$($suffix).nupkg" -Verbosity detailed -ApiKey $nugetApiKey -Source $packageSource
  } else {
    nuget pack $project -IncludeReferencedProjects -Prop configuration=release
    nuget push "$($fileName).nupkg" -Verbosity detailed -ApiKey $nugetApiKey -Source $packageSource
  }
  if ($?) {
    deleteNugetPackage $label $suffix $suffixBuild
  }
}


function fetchNugetVersion ([string] $listSource, [string] $project, [bool] $preRelease) {
  Write-Host "Fetching nuget version from $listSource" -ForegroundColor yellow
  # Use alpha version if the current branch is development
  if ($preRelease) {
    $nug = NuGet list $project -PreRelease -Source $listSource
  } else {
    $nug = NuGet list $project -Source $listSource
  }
  # $nug comes in format: "packageName 1.0.0".
  $version = ($nug.Split(" ") | Select-Object -Last 1)
  # In alpha version the version also includes the string "version-alpha" where version is the semver.
  if ($preRelease) {
    $version = ($version | select -Last 1).Split("-") | select -First 1
  }
  return $version
}

# Determine if it's Pre release
function isPreRelease([string] $branch) {
  switch ($branch) {
    "development" {
      Write-Host "Proceeding script with alpha version for branch: $branch." -ForegroundColor yellow
      $preRelease = 1
    }
    "master" {
      Write-Host "Proceeding script with stable version for branch: $branch." -ForegroundColor yellow
      $preRelease = 0
    }
    default {
      Write-Host "$branch is not a deployable branch exiting..." -ForegroundColor yellow
      exit
    }
  }
}

#####################################################################################################
#                                                                                                   #
#                                                 Start                                             #
#                                                                                                   #
#####################################################################################################
# NuGet package source.
$packageSource = 'https://www.nuget.org/api/v2/package'
# AppVeyor Environmental varible.
$nugetApiKey = $env:NUGET_API_KEY
# AppVeyor Environmental varible.
$branch = $env:APPVEYOR_REPO_BRANCH


# Online NuGet semver
[version] $onlineVersion = fetchNugetVersion 'https://nuget.org/api/v2/' 'Sharpy' $(isPreRelease $branch)
# Local Nuget semver.
[version]$localVersion = Find-Assembly 'AssemblyInformationalVersion'

# Checks if deployment is needed by comparing local and online version
if ($localVersion -gt $onlineVersion) {
  Write-Host "Local version($localVersion) is higher than online version($onlineVersion), proceeding with deployment" -ForegroundColor yellow
    if ($localVersion.Major -gt $onlineVersion.Major) {
      Write-Host 'Validating versioning format' -ForegroundColor yellow
      if ($localVersion.Minor -eq 0) {
        if ($localVersion.Build -eq 0) {
          Write-Host 'Validation Successfull!, deploying major build' -ForegroundColor green
            deployToNuget 'major' $preRelease
        } else {
          throw "Invalid format for Major build, Patch($($localVersion.Build)) need to be set to 0"
        }
      } else {
        throw "Invalid format for Major build, Minor($($localVersion.Minor)) need to be set to 0"
      }
    }
    elseif ($localVersion.Minor -gt $onlineVersion.Minor) {
      Write-Host 'Validating versioning format' -ForegroundColor yellow
      if ($localVersion.Build -eq 0) {
        Write-Host 'Validation Successfull!, deploying minor build' -ForegroundColor green
        deployToNuget 'minor' $preRelease
      } else {
          throw "Invalid format for minor build, patch($($localVersion.Build)) need to be set to 0"
      }
    }
    elseif ($localVersion.Build -gt $onlineVersion.Build) {
      Write-Host 'Deploying patch build' -ForegroundColor yellow
      deployToNuget 'patch' $preRelease
    }

} else {
  Write-Host 'Local version is not greater than online version, no deployment needed' -ForegroundColor yellow
}
