FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Sensor/Sensor.API/Sensor.API.csproj", "Services/Sensor/Sensor.API/"]
RUN dotnet restore "Services/Sensor/Sensor.API/Sensor.API.csproj"
COPY . .
WORKDIR "/src/Services/Sensor/Sensor.API"
RUN dotnet build "Sensor.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sensor.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sensor.API.dll"]