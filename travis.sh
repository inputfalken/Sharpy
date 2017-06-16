#!/bin/bash
set -euo pipefail
IFS=$'\n\t'

yellow=$(tput setaf 3)
bold=$(tput bold)
green=$(tput setaf 2)
reset=$(tput sgr0)
underline=$(tput smul)

####################################################################################################
#                                              Setup
####################################################################################################
mkdir .nuget
echo "${yellow}Downloading NuGet ${reset}"
wget -O .nuget/nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe
getNuget=$?
if [ $getNuget -eq 0 ]; then
  echo "${green}Download Successfull${reset}"
fi

echo "${yellow}Restoring solution with NuGet${reset}"
nuget restore Sharpy.sln -Verbosity quiet
restoreNuget=$?
if [ $restoreNuget -eq 0 ]; then
  echo "${green}Restore Successfull${reset}"
fi

mkdir testrunner
echo "${yellow}Installing NUnit 3.6.1 with NuGet${reset}"
mono .nuget/nuget.exe install NUnit.Runners -Version 3.6.1 -OutputDirectory testrunner -Verbosity quiet
installNUnit=$?
if [ $installNUnit -eq 0 ]; then
  echo "${green}Installation Successfull${reset}"
fi
####################################################################################################
#                                              Build
####################################################################################################
echo "${yellow}Starting build on solution with msbuild ${reset}"
msbuild /v:minimal /p:Configuration=Release Sharpy.sln
buildResult=$?
if [ $buildResult -eq 0 ]; then
  echo "${green}Build Succeeded"
fi
####################################################################################################
#                                              Tests
####################################################################################################
echo "${yellow}Starting NUnit tests${reset}"
mono ./testrunner/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe ./Tests/bin/Release/Tests.dll
####################################################################################################
#                                             AppVeyor
####################################################################################################
BRANCH="$(if [ "$TRAVIS_PULL_REQUEST" == "false" ]; then echo "$TRAVIS_BRANCH"; else echo "$TRAVIS_PULL_REQUEST_BRANCH"; fi)"
# Tell AppVeyor to start a build
curl -d "{accountName: 'inputfalken', projectSlug: 'Sharpy', branch: '$BRANCH'}" -H "Authorization: Bearer $APPVEYOR_API_TOKEN" -H 'Content-Type: application/json' -X POST https://ci.appveyor.com/api/builds
