# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Specify the base images with specific version tags for better reproducibility
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80

# Add labels for better container management
LABEL maintainer="aungmyatkyaw.kk@gmail.com"
LABEL project="OpenHRCore"

# Install necessary packages for Alpine
RUN apk add --no-cache icu-libs

# Create non-root user for security
USER $APP_UID

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /workspace

# Copy csproj files and restore dependencies
COPY ["src/OpenHRCore.API/OpenHRCore.API.csproj", "src/OpenHRCore.API/"]
COPY ["src/OpenHRCore.Application/OpenHRCore.Application.csproj", "src/OpenHRCore.Application/"]
COPY ["src/OpenHRCore.Domain/OpenHRCore.Domain.csproj", "src/OpenHRCore.Domain/"]
COPY ["src/OpenHRCore.Infrastructure/OpenHRCore.Infrastructure.csproj", "src/OpenHRCore.Infrastructure/"]
COPY ["src/OpenHRCore.Shared/OpenHRCore.Shared.csproj", "src/OpenHRCore.Shared/"]
RUN dotnet restore "src/OpenHRCore.API/OpenHRCore.API.csproj"

# Copy everything else
COPY . .
WORKDIR "/workspace/src/OpenHRCore.API"
RUN dotnet build "OpenHRCore.API.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OpenHRCore.API.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode
FROM base AS final
WORKDIR /app

# Add environment variables
ENV ASPNETCORE_URLS=http://*:80
ENV ASPNETCORE_HTTP_PORTS=80
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenHRCore.API.dll"]
