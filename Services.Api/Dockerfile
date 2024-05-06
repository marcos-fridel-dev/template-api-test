#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services.Api/Services.Api.csproj", "Services.Api/"]
COPY ["Application.Data/Application.Data.csproj", "Application.Data/"]
COPY ["Application.UseCases/Application.UseCases.csproj", "Application.UseCases/"]
COPY ["Application.Dto/Application.Dto.csproj", "Application.Dto/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Shared.Common/Shared.Common.csproj", "Shared.Common/"]
COPY ["Shared.Localization/Shared.Localization.csproj", "Shared.Localization/"]
COPY ["Infrastructure.Services/Infrastructure.Services.csproj", "Infrastructure.Services/"]
COPY ["Infrastructure.Persistence/Infrastructure.Persistence.csproj", "Infrastructure.Persistence/"]
COPY ["Application.Services/Application.Services.csproj", "Application.Services/"]
COPY ["Application.Validator/Application.Validator.csproj", "Application.Validator/"]
COPY ["Shared.Resources/Shared.Resources.csproj", "Shared.Resources/"]
RUN dotnet restore "./Services.Api/Services.Api.csproj"
COPY . .
RUN dotnet tool install --global dotnet-ef --version 8.0.200
RUN /root/.dotnet/tools/dotnet-ef database update
WORKDIR "/src/Services.Api"
RUN dotnet build "./Services.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Services.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.Api.dll"]