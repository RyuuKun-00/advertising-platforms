
FROM  mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY AdvertisingPlatforms/*.csproj ./AdvertisingPlatforms/
RUN dotnet restore
COPY . .
WORKDIR "/src/AdvertisingPlatforms"
RUN dotnet build "AdvertisingPlatforms.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdvertisingPlatforms.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE ${BACKEND_EXPOSE_PORT}
ENTRYPOINT ["dotnet", "AdvertisingPlatforms.dll"]