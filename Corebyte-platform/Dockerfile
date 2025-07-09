# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Corebyte-platform/Corebyte-platform.csproj", "Corebyte-platform/"]
RUN dotnet restore "Corebyte-platform/Corebyte-platform.csproj"
COPY . .
WORKDIR "/src/Corebyte-platform"
RUN dotnet build "Corebyte-platform.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Corebyte-platform.csproj" -c Release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Corebyte-platform.dll"]