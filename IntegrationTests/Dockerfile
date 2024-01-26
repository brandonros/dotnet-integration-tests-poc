FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["IntegrationTests.csproj", "./"]
RUN dotnet restore "IntegrationTests.csproj"
COPY . .
RUN dotnet build "IntegrationTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IntegrationTests.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test", "IntegrationTests.dll"]
