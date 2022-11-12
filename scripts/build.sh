#!/bin/bash
MYGET_ENV=""
case "$TRAVIS_BRANCH" in
  "master")
    MYGET_ENV="-dev"
    ;;
esac

cd src
dotnet build -c release
cd ..