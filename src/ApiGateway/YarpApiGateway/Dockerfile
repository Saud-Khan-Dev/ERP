# Build context is the `src` folder, for consistency with the other services:
#   docker build -f ApiGateway/YarpApiGateway/Dockerfile -t erp-yarpapigateway .

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file first so `dotnet restore` is cached until the csproj changes
COPY ["ApiGateway/YarpApiGateway/YarpApiGateway.csproj", "ApiGateway/YarpApiGateway/"]
RUN dotnet restore "ApiGateway/YarpApiGateway/YarpApiGateway.csproj"

COPY ApiGateway/YarpApiGateway/ ApiGateway/YarpApiGateway/
WORKDIR /src/ApiGateway/YarpApiGateway
RUN dotnet build "YarpApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "YarpApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish --no-restore /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YarpApiGateway.dll"]
