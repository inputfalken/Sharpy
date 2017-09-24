[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string] $project,
    [Parameter(Mandatory=$false)]
    [string] $repo = $null,
    [Parameter(Mandatory=$false)]
    [bool] $deploy = $true,
    [Parameter(Mandatory=$false)]
    [bool] $useDocfx = $false,
    [Parameter(Mandatory=$false)]
    [bool] $deletePackage = $false
)
# Pack package to root directory of project and returns the file.
function Pack ([string] $project, [bool] $preRelease, [string] $suffix) {
  $currentDirectory = (Resolve-Path .\).Path
  if ($preRelease) {
    dotnet pack $project --version-suffix $suffix -c Release -o $currentDirectory
  } else {
    dotnet pack $project -c Release -o $currentDirectory
  }
  if (!$?) {
    throw "$project could not be packed by command 'dotnet pack'."
  }
  return [System.IO.FileSystemInfo] (Get-ChildItem *.nupkg | select -First 1)
}
# Deploy package to NuGet.
function Deploy ([string] $package) {
  dotnet nuget push $package -k $env:NUGET_API_KEY -s 'https://www.nuget.org/api/v2/package'
  if (!$?) {
    throw "$project could not be pushed by command 'dotnet nuget push'."
  }
}
# Fetch the online version
function Fetch-OnlineVersion ([string] $listSource, [string] $projectName, [bool] $preRelease) {
  Write-Host "Fetching NuGet Package '$projectName' from $listSource" -ForegroundColor Yellow
  # Use alpha version if the current branch is development.
  if ($preRelease) {
    $packageName = NuGet list $projectName -PreRelease -Source $listSource
  } else {
    $packageName = NuGet list $projectName -Source $listSource
  }

  Write-Host "Found package '$packageName'." -ForegroundColor Green
  # $packageName comes in format: "packageName 1.0.0" and can contain multiple strings!.
  $version = (($packageName | select -First 1).Split(" ") | Select-Object -Last 1)
  # In alpha version the version also includes the string "version-alpha" where version is the semver.
  if ($preRelease) {
    $version = ($version | select -Last 1).Split("-") | select -First 1
  }
  # A hack to get set the revision property to zero.
  $version = "$version.0"
  return [version] $version
}
# Gets the local csproj version from the tag 'VersionPrefix'
function Get-LocalVersion ([string] $project) {
  [string] $versionNodeValue = ((Select-Xml -Path $project -XPath '//VersionPrefix') | select -ExpandProperty node).InnerText
  $version = "$versionNodeValue.0"
  return [version] $version
}
# Updates DocFx documentation.
function Update-GHPages {
  & nuget install docfx.console -Version 2.24.0
  & docfx.console.2.24.0\tools\docfx docfx.json
  if ($lastexitcode -ne 0) {
    throw [System.Exception] "docfx build failed with exit code $lastexitcode."
  }
  git config --global credential.helper store
  Add-Content "$env:USERPROFILE\.git-credentials" "https://$($env:GITHUB_ACCESS_TOKEN):x-oauth-basic@github.com`n"
  git config --global user.email \<\>
  git config --global user.name 'CI'

  git clone $repo -b gh-pages origin_site -q
  Copy-Item origin_site/.git _site -recurse
  Push-Location _site
  git add -A 2>&1
  git commit -m "CI Updates" -q
  git push origin gh-pages -q
  Pop-Location

  # clean up
  git clean -fxd
}
# Deletes the last package
function Delete-Package ([string] $package, [string] $source = 'https://www.nuget.org/api/v2/package') {
  # Splits the string into [name, version & suffix].
  $sections = $package.Split('-')
  $packageName = $sections[0]
  $version = $sections[1..$sections.length] -join '-'
  Write-Host "Attempting to delete package '$packageName', version '$version'."
  NuGet delete $packageName $version -NonInteractive -Source $source -ApiKey $env:NUGET_API_KEY
  if (!$?) {
    throw "Could not delete package '$packageName' version '$version'."
  } else {
    Write-Host "Successfully deleted package '$packageName' version '$version'" -ForegroundColor Green
  }
}

function Start-Deployment ([bool] $preRelease, [string] $suffix = 'alpha') {
  [string] $name = $project.SubString(0, $project.Length - 7).split('\\') | select -last 1
  [version] $onlineVersion = Fetch-OnlineVersion 'https://nuget.org/api/v2/' $name $preRelease
  [version] $localVersion = Get-LocalVersion $project

  Write-Host "Comparing Local $name version '$localVersion' with online $name version '$onlineVersion'" -ForegroundColor Yellow
  if ($localVersion -gt $onlineVersion) {
    Write-Host "Local version '$localVersion' is greater than the online version '$onlineVersion', continuing..." -ForegroundColor Yellow
    if ($deploy) {
      $nupkg = Pack $project $preRelease $suffix
      Deploy $nupkg.Name
      if ($deletePackage) {
        if ($preRelease) {
          Delete-Package "$($name)-$($onlineVersion)-$suffix"
        } else {
          Delete-Package "$($name)-$($onlineVersion)"
        }
      }
    }
    if ($useDocfx) {
      Update-GHPages
    }
  } else {
    Write-Host "Local version '$localVersion' is not greater than online version '$onlineVersion', skipping deployment" -ForegroundColor Yellow
  }
}
####################################################################################################
#                                              Start                                               #
####################################################################################################
Write-Host "Starting script for project '$project'" -ForegroundColor Green
switch ($env:APPVEYOR_REPO_BRANCH) {
  'development' {
    Write-Host "Proceeding script with alpha version for branch: $branch." -ForegroundColor Yellow
    Start-Deployment -PreRelease $true
  }
  'master' {
    Write-Host "Proceeding script with stable version for branch: $branch." -ForegroundColor Yellow
    Start-Deployment -PreRelease $false
  }
  [string]::Empty {
    throw 'Branch can not be empty'
  }
  default {
    Write-Host "$branch is not a deployable branch exiting..." -ForegroundColor Yellow
    exit
  }
}
