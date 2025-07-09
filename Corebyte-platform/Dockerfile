# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Show directory structure for debugging
RUN pwd && ls -la

# Copy project file
COPY ["Corebyte-platform.csproj", "./"]

# Check if file was copied
RUN echo "Checking if project file exists..." && \
    if [ ! -f "Corebyte-platform.csproj" ]; then \
        echo "ERROR: Project file not found!"; \
        ls -la; \
        exit 1; \
    fi

# Restore with detailed output
RUN echo "=== DOTNET INFO ===" && \
    dotnet --info && \
    echo "=== RESTORING PACKAGES ===" && \
    dotnet restore "Corebyte-platform.csproj" -v n || (echo "=== RESTORE FAILED ===" && exit 1)

# Copy remaining files
COPY . .

# Build with detailed output
RUN echo "=== BUILDING APPLICATION ===" && \
    dotnet build "Corebyte-platform.csproj" -c Release -o /app/build --no-restore -v n || (echo "=== BUILD FAILED ===" && exit 1)

# Publish stage
FROM build AS publish
RUN echo "=== PUBLISHING APPLICATION ===" && \
    dotnet publish "Corebyte-platform.csproj" -c Release -o /app/publish --no-restore --no-build -v n || (echo "=== PUBLISH FAILED ===" && exit 1)

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published output
COPY --from=publish /app/publish .

# Verify files were copied
RUN echo "=== PUBLISHED FILES ===" && \
    ls -la

# Set the entry point
ENTRYPOINT ["dotnet", "Corebyte-platform.dll"]