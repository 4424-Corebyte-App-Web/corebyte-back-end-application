# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file
COPY ["Corebyte-platform.csproj", "./"]

# Restore packages
RUN echo "=== RESTORING PACKAGES ===" && \
    dotnet restore "Corebyte-platform.csproj"

# Copy remaining source code
COPY . .

# Build and publish
RUN echo "=== BUILDING AND PUBLISHING ===" && \
    dotnet publish "Corebyte-platform.csproj" -c Release -o /app/publish \
    --no-restore \
    --no-self-contained \
    --runtime linux-x64

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy published app
COPY --from=build /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "Corebyte-platform.dll"]