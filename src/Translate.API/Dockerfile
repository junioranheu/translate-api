#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Translate.API/Translate.API.csproj", "src/Translate.API/"]
COPY ["src/Translate.Application/Translate.Application.csproj", "src/Translate.Application/"]
COPY ["src/Translate.Domain/Translate.Domain.csproj", "src/Translate.Domain/"]
COPY ["src/Translate.Infrastructure/Translate.Infrastructure.csproj", "src/Translate.Infrastructure/"]
RUN dotnet restore "./src/Translate.API/Translate.API.csproj"
COPY . .
WORKDIR "/src/src/Translate.API"
RUN dotnet build "./Translate.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Translate.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Translate.API.dll"]