FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5270
EXPOSE 5271

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BE.TradeeHub.CustomerService.Application/BE.TradeeHub.CustomerService.Application.csproj", "BE.TradeeHub.CustomerService.Application/"]
COPY ["BE.TradeeHub.CustomerService.Infrastructure/BE.TradeeHub.CustomerService.Infrastructure.csproj", "BE.TradeeHub.CustomerService.Infrastructure/"]
RUN dotnet restore "BE.TradeeHub.CustomerService.Application/BE.TradeeHub.CustomerService.Application.csproj"
COPY . .
WORKDIR "/src/BE.TradeeHub.CustomerService.Application"
RUN dotnet build "BE.TradeeHub.CustomerService.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BE.TradeeHub.CustomerService.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BE.TradeeHub.CustomerService.Application.dll"]
