FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 82

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Profile/Profile.API/Profile.API.csproj", "Services/Profile/Profile.API/"]
RUN dotnet restore "Services/Profile/Profile.API/Profile.API.csproj"
COPY . .
WORKDIR "/src/Services/Profile/Profile.API"
RUN dotnet build "Profile.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Profile.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Profile.API.dll"]