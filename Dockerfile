# Use the official .NET 8.0 runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the .NET 8.0 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["local-ipecho.csproj", "."]
RUN dotnet restore "local-ipecho.csproj"
COPY . .
RUN dotnet build "local-ipecho.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "local-ipecho.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "local-ipecho.dll"]