FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["../Mtim.ProtoHub.WebApp/Mtim.ProtoHub.WebApp.csproj", "../Mtim.ProtoHub.WebApp/"]
RUN dotnet restore "../Mtim.ProtoHub.WebApp/Mtim.ProtoHub.WebApp.csproj"
COPY . .
WORKDIR "/src/../Mtim.ProtoHub.WebApp"
RUN dotnet build "Mtim.ProtoHub.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Mtim.ProtoHub.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mtim.ProtoHub.WebApp.dll"]
