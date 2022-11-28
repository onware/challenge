#!/bin/bash

set -o errexit
set -o nounset

dbPassword="$1"

bash .devcontainer/mssql/postCreateCommand-sql.sh "$dbPassword" './bin/Debug/' './.devcontainer/mssql/'

cd health-path
dotnet restore
cd -

cd health-path/ClientApp
set +o errexit
npmReturnCode=1
while [[ $npmReturnCode != 0 ]]; do
    npm install
    npmReturnCode=$?
done
set -o errexit
cd -
