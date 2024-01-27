FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UnitTests.csproj", "./"]
RUN dotnet restore "UnitTests.csproj"
COPY . .
RUN dotnet build "UnitTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UnitTests.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "test", "UnitTests.dll"]
