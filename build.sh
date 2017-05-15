#!/bin/bash

# only proceed script when started not by pull request (PR)
if [ $TRAVIS_PULL_REQUEST == "true" ]; then
  echo "this is PR, exiting"
  exit 0
fi

# enable error reporting to the console
set -e

echo "Building a Jekyll site"

# build site with jekyll, by default to `_site' folder
bundle exec jekyll build

echo "Removing old files"

# cleanup
rm -rf ../mikeborozdin.github.io.master

echo "Cloning a git repo"

#clone `master' branch of the repository using encrypted GH_TOKEN for authentification
git clone https://${GH_TOKEN}@github.com/mikeborozdin/mikeborozdin.github.io.git ../mikeborozdin.github.io.master

echo "copy generated HTML site to master branch"

# copy generated HTML site to `master' branch
cp -R _site/* ../mikeborozdin.github.io.master

# commit and push generated content to `master' branch
# since repository was cloned in write mode with token auth - we can push there
# cd ../mikeborozdin.github.io.master
# git config user.email "mike.borozdin@gmail.com"
# git config user.name "Mike Borozdin"
# git add -A .
# git commit -a -m "Travis #$TRAVIS_BUILD_NUMBER"
# git push --quiet origin master > /dev/null 2>&1
