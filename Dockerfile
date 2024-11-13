# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Specify the base images with specific version tags for better reproducibility
FROM --platform=$TARGETPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
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
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG TARGETARCH
ARG BUILD_CONFIGURATION=Release
WORKDIR /workspace

# Install necessary build dependencies
RUN apk add --no-cache bash

# Create directory structure first
RUN mkdir -p src/OpenHRCore.API/ \
    src/OpenHRCore.Application/ \
    src/OpenHRCore.Domain/ \
    src/OpenHRCore.Infrastructure/ \
    src/OpenHRCore.Shared/

# Copy project files
COPY ["src/OpenHRCore.API/OpenHRCore.API.csproj", "src/OpenHRCore.API/"]
COPY ["src/OpenHRCore.Application/OpenHRCore.Application.csproj", "src/OpenHRCore.Application/"]
COPY ["src/OpenHRCore.Domain/OpenHRCore.Domain.csproj", "src/OpenHRCore.Domain/"]
COPY ["src/OpenHRCore.Infrastructure/OpenHRCore.Infrastructure.csproj", "src/OpenHRCore.Infrastructure/"]
COPY ["src/OpenHRCore.Shared/OpenHRCore.Shared.csproj", "src/OpenHRCore.Shared/"]

# Restore dependencies
RUN dotnet restore "src/OpenHRCore.API/OpenHRCore.API.csproj" -a $TARGETARCH

# Copy everything else
COPY . .
WORKDIR "/workspace/src/OpenHRCore.API"
RUN dotnet build "OpenHRCore.API.csproj" -c "$BUILD_CONFIGURATION" -o /app/build --no-restore -a $TARGETARCH

# This stage is used to publish the service project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OpenHRCore.API.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish --no-restore -a $TARGETARCH /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app

# Add environment variables
ENV ASPNETCORE_URLS=http://*:80
ENV ASPNETCORE_HTTP_PORTS=80
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenHRCore.API.dll"]
