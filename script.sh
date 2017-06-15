#!/bin/bash
set -euo pipefail
IFS=$'\n\t'

yellow=$(tput setaf 3)
green=$(tput setaf 2)
reset=$(tput sgr0)
underline=$(tput smul)
exitUnderline=$(tput rmul)

echo "${yellow}Starting build on solution with msbuild ${reset}"
msbuild /v:minimal /p:Configuration=Release Sharpy.sln
buildResult=$?
if [ $buildResult -eq 0 ]; then
  echo "${green}Build Succeeded"
fi

echo "${yellow}Starting tests with NUnit${reset}"
mono ./testrunner/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe ./Tests/bin/Release/Tests.dll

#####################
#       Deploy      #
#####################
Source='https://www.nuget.org/api/v2/package'
function deploy {
  mono .nuget/nuget.exe pack ./Sharpy.nuspec -Verbosity detailed

  mono .nuget/nuget.exe push ./Sharpy.*.*.*.nupkg -Verbosity detailed -ApiKey "$NUGET_API_KEY" -Source "$Source"
}

BRANCH="$(if [ "$TRAVIS_PULL_REQUEST" == "false" ]; then echo "$TRAVIS_BRANCH"; else echo "$TRAVIS_PULL_REQUEST_BRANCH"; fi)"

case "$BRANCH" in
  master)
    echo "${underline}Deploying production from branch $BRANCH${exitUnderline}"

    set +e
    grep -vE '<version>.+-alpha' ./Sharpy.nuspec
    isStableVersion=$?
    set -e

    if [ $isStableVersion -eq 0 ]; then
      deploy
    fi
  ;;
  development)
    echo "${underline}Deploying alpha build from branch $BRANCH${exitUnderline}"

    set +e
    grep -E '<version>.+-alpha' ./Sharpy.nuspec
    isAlphaVersion=$?
    set -e

    if [ $isAlphaVersion -eq 0 ]; then
      deploy
    fi
  ;;
  *)
    echo 'Not in a valid branch, skipping deployment.'
  ;;
esac
