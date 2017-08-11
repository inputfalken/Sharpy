# Find assembly version info.
function findAssemblyVersion ([string] $assemblyVersionName) {
  $pattern = '\[assembly: {0}\("(.*)"\)\]' -f $assemblyVersionName
    (Get-Content '.\src\Sharpy\Properties\AssemblyInfo.cs') | ForEach-Object {
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

function updateDocumentation {
  & nuget install docfx.console -Version 2.22.1 -Source https://www.myget.org/F/docfx/api/v3/index.json
  & docfx.console.2.22.1\tools\docfx docfx.json
  if ($lastexitcode -ne 0) {
    throw [System.Exception] "docfx build failed with exit code $lastexitcode."
  }
  git config --global credential.helper store
  Add-Content "$env:USERPROFILE\.git-credentials" "https://$($env:GITHUB_ACCESS_TOKEN):x-oauth-basic@github.com`n"
  git config --global user.email 'sharpy.continuous.integration@gmail.com'
  git config --global user.name 'SharpyCI'

  git clone https://github.com/inputfalken/Sharpy -b gh-pages origin_site -q
  Copy-Item origin_site/.git _site -recurse
  CD _site
  git add -A 2>&1
  git commit -m "CI Updates" -q
  git push origin gh-pages -q
}

# NuGet Deployment
function deployToNuget ([string] $label, [bool] $suffixBuild) {
  $project = '.\src\Sharpy\Sharpy.csproj'
  # The generated file does not share folder as project
  $fileName = ".\Sharpy.$($localVersion.Major).$($localVersion.Minor).$($localVersion.Build)"
  $suffix = 'alpha'

  if ($suffixBuild) {
    nuget pack $project -IncludeReferencedProjects -Prop configuration=release -Suffix $suffix
    nuget push "$($fileName)-$($suffix).nupkg" -Verbosity detailed -ApiKey $nugetApiKey -Source $packageSource
  } else {
    nuget pack $project -IncludeReferencedProjects -Prop configuration=release
    nuget push "$($fileName).nupkg" -Verbosity detailed -ApiKey $nugetApiKey -Source $packageSource
  }
  if ($?) {
    updateDocumentation
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
function isAlphaBranch([string] $branch) {
  switch ($branch) {
    "development" {
      Write-Host "Proceeding script with alpha version for branch: $branch." -ForegroundColor yellow
      return 1
    }
    "master" {
      Write-Host "Proceeding script with stable version for branch: $branch." -ForegroundColor yellow
      return 0
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

[bool] $isAlpha = isAlphaBranch $branch
# Online NuGet semver
[version] $onlineVersion = fetchNugetVersion 'https://nuget.org/api/v2/' 'Sharpy' $isAlpha
# Local Nuget semver.
[version]$localVersion = findAssemblyVersion 'AssemblyInformationalVersion'


# Checks if deployment is needed by comparing local and online version
if ($localVersion -gt $onlineVersion) {
  Write-Host "Local version($localVersion) is higher than online version($onlineVersion), proceeding with deployment" -ForegroundColor yellow
    if ($localVersion.Major -gt $onlineVersion.Major) {
      Write-Host 'Validating versioning format' -ForegroundColor yellow
      if ($localVersion.Minor -eq 0) {
        if ($localVersion.Build -eq 0) {
          Write-Host 'Validation Successfull!, deploying major build' -ForegroundColor green
            deployToNuget 'major' $isAlpha
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
        deployToNuget 'minor' $isAlpha
      } else {
          throw "Invalid format for minor build, patch($($localVersion.Build)) need to be set to 0"
      }
    }
    elseif ($localVersion.Build -gt $onlineVersion.Build) {
      Write-Host 'Deploying patch build' -ForegroundColor yellow
      deployToNuget 'patch' $isAlpha
    }

} else {
  Write-Host 'Local version is not greater than online version, no deployment needed' -ForegroundColor yellow
}
