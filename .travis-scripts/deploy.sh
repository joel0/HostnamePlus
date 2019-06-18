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
echo -n Stopping kesteral-hostname.service...
ssh $deploy_user@$deploy_server sudo systemctl stop kesteral-hostname
echo stopped

rsync -vrlpt \
    --delete \
    --delete-excluded \
    HostnamePlus/publish/ $deploy_user@$deploy_server:~/www/

echo -n Starting kesteral-hostname.service...
ssh $deploy_user@$deploy_server sudo systemctl start kesteral-hostname
echo started
