FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Kowmal.WebApp/Kowmal.WebApp.csproj", "Kowmal.WebApp/"]
RUN dotnet restore "Kowmal.WebApp/Kowmal.WebApp.csproj"
COPY . .
WORKDIR "/src/Kowmal.WebApp"
RUN dotnet build "Kowmal.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Kowmal.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kowmal.WebApp.dll"]
