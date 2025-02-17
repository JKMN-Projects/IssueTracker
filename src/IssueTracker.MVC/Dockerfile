# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#
## This stage is used to build the service project
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["src/IssueTracker.MVC/IssueTracker.MVC.csproj", "src/IssueTracker.MVC/"]
#COPY ["src/IssueTracker.Domain/IssueTracker.Domain.csproj", "src/IssueTracker.Domain/"]
#RUN dotnet restore "./src/IssueTracker.MVC/IssueTracker.MVC.csproj"
#COPY . .
#WORKDIR "/src/src/IssueTracker.MVC"
#RUN dotnet build "./IssueTracker.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## This stage is used to publish the service project to be copied to the final stage
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./IssueTracker.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "IssueTracker.MVC.dll"]

# Syntax version
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy only the MVC project files first
COPY src/IssueTracker.MVC/*.csproj ./
RUN dotnet restore

# Copy the rest of the MVC application
COPY src/IssueTracker.MVC/. ./

# Build and publish
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Configure Aspire service discovery
ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
ENV ServiceDefaults__BaseAddress=http://+:80

# Set up healthcheck
#HEALTHCHECK --interval=30s --timeout=3s \
    #CMD curl -f http://localhost:80/health || exit 1

# Expose port
EXPOSE 80

ENTRYPOINT ["dotnet", "IssueTracker.MVC.dll"]