#!/bin/bash

set -o errexit
set -o nounset

dbPassword="$1"

bash .devcontainer/mssql/postCreateCommand-sql.sh "$dbPassword" './bin/Debug/' './.devcontainer/mssql/'

cd health-path
dotnet restore
cd -

cd health-path/ClientApp
npm install
cd -
