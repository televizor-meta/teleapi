FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Rostelecom.DigitalProfile.Api/Rostelecom.DigitalProfile.Api.csproj", "Rostelecom.DigitalProfile.Api/"]
RUN dotnet restore "Rostelecom.DigitalProfile.Api/Rostelecom.DigitalProfile.Api.csproj"
COPY . .
WORKDIR "/src/Rostelecom.DigitalProfile.Api"
RUN dotnet build "Rostelecom.DigitalProfile.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rostelecom.DigitalProfile.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rostelecom.DigitalProfile.Api.dll"]
