FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BE.TradeeHub.CustomerService.Application/BE.TradeeHub.CustomerService.Application.csproj", "BE.TradeeHub.CustomerService.Application/"]
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
