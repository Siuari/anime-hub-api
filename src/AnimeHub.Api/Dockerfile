# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AnimeHub.Api/AnimeHub.Api.csproj", "src/AnimeHub.Api/"]
COPY ["src/AnimeHub.Infra.IoC/AnimeHub.Infra.IoC.csproj", "src/AnimeHub.Infra.IoC/"]
COPY ["src/AnimeHub.Infra.Data/AnimeHub.Infra.Data.csproj", "src/AnimeHub.Infra.Data/"]
COPY ["src/AnimeHub.Application/AnimeHub.Application.csproj", "src/AnimeHub.Application/"]
COPY ["src/AnimeHub.Domain/AnimeHub.Domain.csproj", "src/AnimeHub.Domain/"]
RUN dotnet restore "./src/AnimeHub.Api/AnimeHub.Api.csproj"
COPY . .
WORKDIR "/src/src/AnimeHub.Api"
RUN dotnet build "./AnimeHub.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AnimeHub.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnimeHub.Api.dll"]