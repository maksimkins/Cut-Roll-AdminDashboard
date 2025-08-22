FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY ./Cut-Roll-AdminDashboard/src/Cut-Roll-AdminDashboard.Api/*.csproj .Cut-Roll-AdminDashboard/src/Cut-Roll-AdminDashboard.Api/
COPY ./Cut-Roll-AdminDashboard/src/Cut-Roll-AdminDashboard.Infrastructure/*.csproj .Cut-Roll-AdminDashboard/src/Cut-Roll-AdminDashboard.Infrastructure/
COPY ./Cut-Roll-AdminDashboard/src/Cut-Roll-AdminDashboard.Core/*.csproj .Cut-Roll-AdminDashboard/src/Cut-Roll-AdminDashboard.Core/

COPY . .

RUN dotnet publish Cut-Roll-AdminDashboard/src/Cut-Roll-AdminDashboard.Api/Cut-Roll-AdminDashboard.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT [ "dotnet", "Cut-Roll-AdminDashboard.Api.dll" ]