FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /src

COPY . .
RUN dotnet restore "./ParkBee.Assessment.API/ParkBee.Assessment.API.csproj" --configfile "./nuget.config"
WORKDIR "/src/ParkBee.Assessment.API"
RUN dotnet publish "ParkBee.Assessment.API.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app .
EXPOSE 49835
ENTRYPOINT ["dotnet", "ParkBee.Assessment.API.dll", "--urls=http://0.0.0.0:49835"]