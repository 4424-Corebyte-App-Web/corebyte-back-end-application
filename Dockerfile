# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution and project files
COPY *.sln .
COPY Corebyte-platform/Corebyte-platform.csproj ./Corebyte-platform/
RUN dotnet restore Corebyte-platform/Corebyte-platform.csproj

# Copy everything else and build
COPY . .
WORKDIR /app/Corebyte-platform
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/Corebyte-platform/out .

# Expose the port your app runs on
EXPOSE 80

# Define the entry point
ENTRYPOINT ["dotnet", "Corebyte-platform.dll"]
