#!/bin/bash

# Exit script if any error occurs
set -e

# # Prepare
# dotnet restore

# # Build
# dotnet build -c Release

# # Bundle
# dotnet publish -c Release -o ./publish

# Copy to production server
ssh $deploy_user@$deploy_server sudo systemctl stop kesteral-hostname
rsync -vrlpt \
    --delete \
    --delete-excluded \
    ./publish/ $deploy_user@$deploy_server:~/www/
ssh $deploy_user@$deploy_server sudo systemctl start kesteral-hostname
