#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Marketplace.Services.Organizations/Marketplace.Services.Organizations.csproj", "Marketplace.Services.Organizations/"]
COPY ["Marketplace.Common/Marketplace.Common.csproj", "Marketplace.Common/"]
RUN dotnet restore "Marketplace.Services.Organizations/Marketplace.Services.Organizations.csproj"
COPY . .
WORKDIR "/src/Marketplace.Services.Organizations"
RUN dotnet build "Marketplace.Services.Organizations.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Marketplace.Services.Organizations.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Marketplace.Services.Organizations.dll"]