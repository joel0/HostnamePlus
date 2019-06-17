#!/bin/bash

# Exit script if any error occurs
#set -e
echo testing
# cd $TRAVIS_BUILD_DIR

# # Prepare
# dotnet restore

# # Build
# dotnet build -c Release

# # Bundle
# dotnet publish -c Release -o ./publish

# # Copy to production server
# ssh $deploy_user@$deploy_server sudo systemctl stop kesteral-hostname
# rsync -vrlpt \
#     --delete \
#     --delete-excluded \
#     ./HostnamePlus/publish/ $deploy_user@$deploy_server:~/www/
# ssh $deploy_user@$deploy_server sudo systemctl start kesteral-hostname
