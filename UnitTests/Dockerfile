FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UnitTests/UnitTests.csproj", "UnitTests/"]
RUN dotnet restore "UnitTests/UnitTests.csproj"
COPY . .
WORKDIR /src/UnitTests
RUN dotnet build "UnitTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UnitTests.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test", "/app/UnitTests.dll"]
