FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IntegrationTests/IntegrationTests.csproj", "IntegrationTests/"]
RUN dotnet restore "IntegrationTests/IntegrationTests.csproj"
COPY . .
WORKDIR /src/IntegrationTests
RUN dotnet build "IntegrationTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IntegrationTests.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test", "/app/IntegrationTests.dll"]
