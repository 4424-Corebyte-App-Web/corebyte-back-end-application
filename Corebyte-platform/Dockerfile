# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy just the project file and restore dependencies
COPY ["Corebyte-platform.csproj", "./"]
RUN dotnet restore "Corebyte-platform.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "Corebyte-platform.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Corebyte-platform.csproj" -c Release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Corebyte-platform.dll"]