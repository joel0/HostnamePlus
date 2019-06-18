#!/bin/bash

# Exit script if any error occurs
set -e

# Bundle a release to deploy
dotnet publish -c Release -o ./publish $TRAVIS_BUILD_DIR

# Copy to production server
echo -n Stopping $deploy_service...
ssh $deploy_user@$deploy_server sudo systemctl stop $deploy_service
echo stopped

rsync -vrlpt \
    --delete \
    --delete-excluded \
    HostnamePlus/publish/ $deploy_user@$deploy_server:~/www/

echo -n Starting $deploy_service...
ssh $deploy_user@$deploy_server sudo systemctl start $deploy_service
echo started
