FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["../Mtim.Grpc.Modbus/Mtim.Grpc.Modbus.csproj", "../Mtim.Grpc.Modbus/"]
RUN dotnet restore "../Mtim.Grpc.Modbus/Mtim.Grpc.Modbus.csproj"
COPY . .
WORKDIR "/src/../Mtim.Grpc.Modbus"
RUN dotnet build "Mtim.Grpc.Modbus.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Mtim.Grpc.Modbus.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mtim.Grpc.Modbus.dll"]
