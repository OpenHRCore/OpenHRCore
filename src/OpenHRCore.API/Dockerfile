# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/OpenHRCore.API/OpenHRCore.API.csproj", "src/OpenHRCore.API/"]
COPY ["src/OpenHRCore.Application/OpenHRCore.Application.csproj", "src/OpenHRCore.Application/"]
COPY ["src/OpenHRCore.Domain/OpenHRCore.Domain.csproj", "src/OpenHRCore.Domain/"]
COPY ["src/SharedKernel/OpenHRCore.SharedKernel.Domain/OpenHRCore.SharedKernel.Domain.csproj", "src/SharedKernel/OpenHRCore.SharedKernel.Domain/"]
COPY ["src/SharedKernel/OpenHRCore.SharedKernel.Application/OpenHRCore.SharedKernel.Application.csproj", "src/SharedKernel/OpenHRCore.SharedKernel.Application/"]
COPY ["src/SharedKernel/OpenHRCore.SharedKernel.Utilities/OpenHRCore.SharedKernel.Utilities.csproj", "src/SharedKernel/OpenHRCore.SharedKernel.Utilities/"]
COPY ["src/OpenHRCore.Infrastructure/OpenHRCore.Infrastructure.csproj", "src/OpenHRCore.Infrastructure/"]
COPY ["src/SharedKernel/OpenHRCore.SharedKernel.Infrastructure/OpenHRCore.SharedKernel.Infrastructure.csproj", "src/SharedKernel/OpenHRCore.SharedKernel.Infrastructure/"]
RUN dotnet restore "./src/OpenHRCore.API/OpenHRCore.API.csproj"
COPY . .
WORKDIR "/src/src/OpenHRCore.API"
RUN dotnet build "./OpenHRCore.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OpenHRCore.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OpenHRCore.API.dll"]