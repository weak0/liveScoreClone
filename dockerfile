# Use .NET 8.0 Runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use .NET 8.0 SDK for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file(s) and restore dependencies
COPY ["LiveScoreReporter/LiveScoreReporter.csproj", "LiveScoreReporter/"]
RUN dotnet restore "LiveScoreReporter/LiveScoreReporter.csproj"

# Copy the entire project and publish it
COPY . .
WORKDIR "/src/LiveScoreReporter"
RUN dotnet publish "LiveScoreReporter.csproj" -c Release -o /app/publish

# Final stage with runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "LiveScoreReporter.dll"]
