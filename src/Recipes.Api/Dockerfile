FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Recipes.Api/Recipes.Api.csproj", "src/Recipes.Api/"]
COPY ["src/Recipes.Domain/Recipes.Domain.csproj", "src/Recipes.Domain/"]
COPY ["src/Recipes.Infrastructure/Recipes.Infrastructure.csproj", "src/Recipes.Infrastructure/"]
RUN dotnet restore "src/Recipes.Api/Recipes.Api.csproj"
COPY . .
WORKDIR "/src/src/Recipes.Api"
RUN dotnet build "Recipes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Recipes.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Recipes.Api.dll"]
