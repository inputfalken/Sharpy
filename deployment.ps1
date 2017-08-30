param([string] $project, [string] $repo)
# Pack package to root directory of project and returns the file.
function Pack ([string] $project, [bool] $isAlpha) {
  $currentDirectory = (Resolve-Path .\).Path
  if ($isAlpha) {
    dotnet pack $project --version-suffix alpha -c Release -o $currentDirectory
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
# If returns true if the branch is development and false if it's master.
function Is-Alpha([string] $branch) {
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
# Fetch the online version
function Fetch-OnlineVersion ([string] $listSource, [string] $projectName, [bool] $isAlpha) {
  Write-Host "Fetching NuGet version from $listSource" -ForegroundColor yellow
  # Use alpha version if the current branch is development.
  if ($isAlpha) {
    $packageName = NuGet list $projectName -PreRelease -Source $listSource
  } else {
    $packageName = NuGet list $projectName -Source $listSource
  }
  # $packageName comes in format: "packageName 1.0.0".
  $version = ($packageName.Split(" ") | Select-Object -Last 1)
  # In alpha version the version also includes the string "version-alpha" where version is the semver.
  if ($isAlpha) {
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
  & nuget install docfx.console -Version 2.23.1 -Source https://www.myget.org/F/docfx/api/v3/index.json
  & docfx.console.2.23.1\tools\docfx docfx.json
  if ($lastexitcode -ne 0) {
    throw [System.Exception] "docfx build failed with exit code $lastexitcode."
  }
  git config --global credential.helper store
  Add-Content "$env:USERPROFILE\.git-credentials" "https://$($env:GITHUB_ACCESS_TOKEN):x-oauth-basic@github.com`n"
  git config --global user.email \<\>
  git config --global user.name 'CI'

  git clone $repo -b gh-pages origin_site -q
  Copy-Item origin_site/.git _site -recurse
  CD _site
  git add -A 2>&1
  git commit -m "CI Updates" -q
  git push origin gh-pages -q
}

[string] $branch = $env:APPVEYOR_REPO_BRANCH
[bool] $isAlpha = Is-Alpha $branch
[string] $name = ($project.Split('\\') | select -last 1).Split('.') | select -first 1
[version] $onlineVersion = Fetch-OnlineVersion 'https://nuget.org/api/v2/' $name $isAlpha
[version] $localVersion = Get-LocalVersion $project

Write-Host "Comparing Local version($localVersion) with online version($onlineVersion)"
if ($localVersion -gt $onlineVersion) {
  Write-Host "Local version($localVersion) is greater than the online version($onlineVersion), performing deployment" -ForegroundColor Yellow
  $nupkg = Pack $project $isAlpha
  Deploy $nupkg.Name
  # If it's the master branch
  #if(!$isAlpha) {
    Update-GHPages
  #} else {
  #  Write-Host "Alpha version, skipping documentation update." -ForegroundColor Yellow
  #}
  Delete-OnlinePackage $nupkg
} else {
  Write-Host "Local version($localVersion) is not greater than online version($onlineVersion), skipping deployment" -ForegroundColor Yellow
}
