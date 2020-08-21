# Dockerfile
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app
COPY . ./

# Run tests
RUN dotnet test Administration/UnitTests/UnitTests.csproj
RUN dotnet test Streaming/UnitTests/UnitTests.csproj

RUN dotnet publish -c Release -o out
RUN mkdir /app/Web/out/AudioFiles

WORKDIR /app/Web
RUN dotnet ef migrations add DockerDotnet
RUN dotnet ef database update

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/Web/out .
COPY --from=build-env /app/Web/dubbing.db .
ENTRYPOINT ["dotnet", "Web.dll"]
