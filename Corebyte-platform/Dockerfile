# Use the .NET 9.0 SDK image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install runtime dependencies
RUN apt-get update && apt-get install -y --no-install-recommends \
    libc6 \
    libgcc1 \
    libgssapi-krb5-2 \
    libicu-dev \
    libssl3 \
    libstdc++6 \
    zlib1g \
    && rm -rf /var/lib/apt/lists/*

# Copy the published app
COPY --from=build /app/out .

# Configure environment variables
ENV ASPNETCORE_URLS=http://+:80;https://+:443
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DB_HOST=mysql.railway.internal
ENV DB_PORT=3306
ENV DB_NAME=railway
ENV DB_USER=root
ENV DB_PASSWORD=irAMqRCPMSxkXReoLjmgcSplgkRfizkR

# Set the entry point
ENTRYPOINT ["dotnet", "Corebyte-platform.dll"]
