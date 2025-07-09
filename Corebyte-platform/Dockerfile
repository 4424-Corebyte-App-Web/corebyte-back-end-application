# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# List directory contents for debugging
RUN ls -la

# Copy just the project file and restore dependencies
COPY ["Corebyte-platform.csproj", "./"]

# Enable detailed logging for dotnet restore
RUN dotnet --info && \
    dotnet restore "Corebyte-platform.csproj" -v detailed

# Copy everything else and build
COPY . .
RUN dotnet build "Corebyte-platform.csproj" -c Release -o /app/build --no-restore

# Publish stage
FROM build AS publish
RUN dotnet publish "Corebyte-platform.csproj" -c Release -o /app/publish --no-restore --no-build

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "Corebyte-platform.dll"]