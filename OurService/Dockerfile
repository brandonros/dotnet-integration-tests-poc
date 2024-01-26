FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OurService.csproj", "./"]
RUN dotnet restore "OurService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "OurService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OurService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OurService.dll"]
