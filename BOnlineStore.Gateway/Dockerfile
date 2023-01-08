#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BOnlineStore.Gateway/BOnlineStore.Gateway.csproj", "BOnlineStore.Gateway/"]
COPY ["Shared/BOnlineStore.Shared/BOnlineStore.Shared.csproj", "Shared/BOnlineStore.Shared/"]
COPY ["Shared/BOnlineStore.Localization/BOnlineStore.Localization.csproj", "Shared/BOnlineStore.Localization/"]
RUN dotnet restore "BOnlineStore.Gateway/BOnlineStore.Gateway.csproj"
COPY . .
WORKDIR "/src/BOnlineStore.Gateway"
RUN dotnet build "BOnlineStore.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BOnlineStore.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BOnlineStore.Gateway.dll"]