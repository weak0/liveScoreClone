#!/bin/bash

GREEN='\033[0;32m'

print_message() {
    local color=$1
    local message=$2
    echo -e "${color}\033[1m$(echo $message | tr '[:lower:]' '[:upper:]')${RESET}"
}

# Step 1: Create a custom Docker network (optional but recommended)
docker network create my-network

# Step 2: Run SQL Server container
echo "Starting SQL Server container..."
docker run -d \
  --network my-network \
  -e "SA_PASSWORD=YourStrongPassword!" \
  -e "ACCEPT_EULA=Y" \
  -p 1433:1433 \
  --name baza_danych \
  mcr.microsoft.com/mssql/server:2022-latest

  sleep 5s

# Step 4: Run API container (replace with your Docker Hub image)
echo "Starting API container..."
docker run -d \
  --network my-network \
  -p 5000:8080 \
  --name api-container \
  maciogo42/livescore2024:latest

print_message $GREEN "wejdz na localhost:5000/index.html"

sleep 30s