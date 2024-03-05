FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY . .

RUN dotnet restore

RUN dotnet build --configuration Release --no-restore

RUN dotnet publish --configuration Release --no-restore --output ./publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app

COPY ["aspnetapp.pfx", "/https/aspnetapp.pfx"]

COPY --from=build /app/publish .

EXPOSE 5001
EXPOSE 5000

ENTRYPOINT ["dotnet", "user.dll"]