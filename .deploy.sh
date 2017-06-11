#!/bin/bash
set -euo pipefail
IFS=$'\n\t'

Source='https://www.nuget.org/api/v2/package'

# Change tresureGen to use my own path.
function deploy {
  mono .nuget/nuget.exe pack ./Sharpy.nuspec -Verbosity detailed

  mono .nuget/nuget.exe push ./Sharpy.*.*.*.nupkg -Verbosity detailed -ApiKey "$NUGET_API_KEY" -Source "$Source"
}

BRANCH="$(if [ "$TRAVIS_PULL_REQUEST" == "false" ]; then echo "$TRAVIS_BRANCH"; else echo "$TRAVIS_PULL_REQUEST_BRANCH"; fi)"

case "$BRANCH" in
  master)
    echo "Deploying production from branch $BRANCH"

    set +e
    grep -vE '<version>.+-alpha' ./Sharpy.nuspec
    isStableVersion=$?
    set -e

    if [ $isStableVersion -eq 0 ]; then
      deploy
    fi
  ;;
  development)
    echo "Deploying alpha build from branch $BRANCH"

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
