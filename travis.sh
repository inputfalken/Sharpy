#!/bin/bash
set -euo pipefail
IFS=$'\n\t'

yellow=$(tput setaf 3)
green=$(tput setaf 2)
reset=$(tput sgr0)

echo "${yellow}Running dotnet restore ${reset}"
if dotnet restore ./Sharpy.sln -v m ; then
  echo "${green}Restoration succeeded"
fi
####################################################################################################
#                                                                                                  #
#                                              Build                                               #
#                                                                                                  #
####################################################################################################
echo "${yellow}Running dotnet build ${reset}"
if dotnet build ./Sharpy.sln -c Release ; then
  echo "${green}Build Succeeded"
fi
####################################################################################################
#                                                                                                  #
#                                              Tests                                               #
#                                                                                                  #
####################################################################################################
echo "${yellow}Running dotnet test${reset}"
find tests -name '*.csproj' -exec dotnet test {} -c Release --no-build \;
####################################################################################################
#                                                                                                  #
#                                             AppVeyor                                             #
#                                                                                                  #
####################################################################################################
BRANCH="$(if [ "$TRAVIS_PULL_REQUEST" == "false" ]; then echo "$TRAVIS_BRANCH"; else echo "$TRAVIS_PULL_REQUEST_BRANCH"; fi)"
ACCOUNT_NAME=$(echo $TRAVIS_REPO_SLUG | cut -d '/' -f 1)
REPO_NAME=$(echo $TRAVIS_REPO_SLUG | cut -d '/' -f 2)
# Tell AppVeyor to start a build
#curl -d "{accountName: '$ACCOUNT_NAME', projectSlug: '$REPO_NAME', branch: '$BRANCH'}" -H "Authorization: Bearer $APPVEYOR_API_TOKEN" -H 'Content-Type: application/json' -X POST https://ci.appveyor.com/api/builds
