#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /
ADD Shared/Certificates/bonlinestore.crt /usr/local/share/ca-certificates/bonlinestore.crt
RUN chmod 644 /usr/local/share/ca-certificates/bonlinestore.crt && update-ca-certificates

WORKDIR /app
ADD Shared/Certificates/bonlinestore.pfx bonlinestore.pfx

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BOnlineStore.Services/Production/BOnlineStore.Services.Production.Api/BOnlineStore.Services.Production.Api.csproj", "BOnlineStore.Services/Production/BOnlineStore.Services.Production.Api/"]
COPY ["Shared/BOnlineStore.Generic.Service/BOnlineStore.Generic.Service.csproj", "Shared/BOnlineStore.Generic.Service/"]
COPY ["Shared/BOnlineStore.MongoDb.GenericRepository/BOnlineStore.MongoDb.GenericRepository.csproj", "Shared/BOnlineStore.MongoDb.GenericRepository/"]
COPY ["Shared/BOnlineStore.Shared/BOnlineStore.Shared.csproj", "Shared/BOnlineStore.Shared/"]
COPY ["Shared/BOnlineStore.Localization/BOnlineStore.Localization.csproj", "Shared/BOnlineStore.Localization/"]
RUN dotnet restore "BOnlineStore.Services/Production/BOnlineStore.Services.Production.Api/BOnlineStore.Services.Production.Api.csproj"
COPY . .
WORKDIR "/src/BOnlineStore.Services/Production/BOnlineStore.Services.Production.Api"
RUN dotnet build "BOnlineStore.Services.Production.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BOnlineStore.Services.Production.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BOnlineStore.Services.Production.Api.dll"]