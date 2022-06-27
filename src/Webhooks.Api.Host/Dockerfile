#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Webhooks.Api.Host/Webhooks.Api.Host.csproj", "Webhooks.Api.Host/"]
COPY ["Webhooks.Services/Webhooks.Services.csproj", "Webhooks.Services/"]
COPY ["Webhooks.DataAccess/Webhooks.DataAccess.csproj", "Webhooks.DataAccess/"]
COPY ["Webhooks.Models/Webhooks.Models.csproj", "Webhooks.Models/"]
COPY ["Webhooks.DataAccess.Models/Webhooks.DataAccess.Models.csproj", "Webhooks.DataAccess.Models/"]
COPY ["Webhooks.Infrastructure/Webhooks.Infrastructure.csproj", "Webhooks.Infrastructure/"]
RUN dotnet restore "Webhooks.Api.Host/Webhooks.Api.Host.csproj"
COPY . .
WORKDIR "/src/Webhooks.Api.Host"
RUN dotnet build "Webhooks.Api.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Webhooks.Api.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Webhooks.Api.Host.dll"]