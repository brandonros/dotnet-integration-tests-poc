# Use the official .NET SDK image to run tests
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set the working directory in the container
WORKDIR /tests

# Copy the .csproj file(s) and restore any dependencies
# Note: Adjust the path if your .csproj files are in a subdirectory
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining test project files into the container
COPY . ./

# Define the entry point command to run tests
ENTRYPOINT ["dotnet", "test"]
