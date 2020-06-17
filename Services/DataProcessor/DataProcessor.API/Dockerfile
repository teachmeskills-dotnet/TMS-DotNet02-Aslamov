FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 81

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/DataProcessor/DataProcessor.API/DataProcessor.API.csproj", "Services/DataProcessor/DataProcessor.API/"]
RUN dotnet restore "Services/DataProcessor/DataProcessor.API/DataProcessor.API.csproj"
COPY . .
WORKDIR "/src/Services/DataProcessor/DataProcessor.API"
RUN dotnet build "DataProcessor.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataProcessor.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataProcessor.API.dll"]