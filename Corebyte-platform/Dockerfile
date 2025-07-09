# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy only the project file first to leverage Docker cache
COPY ["Corebyte-platform.csproj", "./"]

# Print .NET version and list files for debugging
RUN dotnet --version && \
    dotnet --list-sdks && \
    ls -la

# Restore with detailed diagnostics
RUN dotnet restore "Corebyte-platform.csproj" --verbosity detailed

# Copy everything else and build
COPY . .
WORKDIR "/src"
RUN dotnet build "Corebyte-platform.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Corebyte-platform.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install CA certificates and other dependencies
RUN apt-get update && apt-get install -y --no-install-recommends \
    libc6 \
    libgcc1 \
    libgssapi-krb5-2 \
    libicu-dev \
    libssl3 \
    libstdc++6 \
    zlib1g \
    && rm -rf /var/lib/apt/lists/*

# Copy the published application
COPY --from=publish /app/publish .

# Set environment variables with defaults
ENV DB_HOST=mysql.railway.internal
ENV DB_PORT=3306
ENV DB_NAME=railway
ENV DB_USER=root
ENV DB_PASSWORD=irAMqRCPMSxkXReoLjmgcSplgkRfizkR
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80;https://+:443

# Set the entry point
ENTRYPOINT ["dotnet", "Corebyte-platform.dll"]
