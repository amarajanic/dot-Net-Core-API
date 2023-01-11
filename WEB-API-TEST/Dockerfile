FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.sln .
COPY *.csproj ./


COPY /. ./
WORKDIR /app
RUN dotnet publish -c Release
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS publish-env
WORKDIR /app
COPY --from=build-env /app /app
ENTRYPOINT ["dotnet", "--info"]