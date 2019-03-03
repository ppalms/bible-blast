# This is a big fat image that we only want to use for building, not production
FROM microsoft/dotnet:2.2-sdk AS builder
WORKDIR /sln

COPY ./BibleBlast.API/BibleBlast.API.csproj ./BibleBlast.API/BibleBlast.API.csproj
RUN dotnet restore "./BibleBlast.API/BibleBlast.API.csproj"

COPY . .
WORKDIR /sln/BibleBlast.API
RUN dotnet build -c Release --no-restore
RUN dotnet publish -c Release -o "../dist" --no-restore

# Generate final app image from build image
FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT Development
ENV ASPNETCORE_URLS http://0.0.0.0:8080
ENTRYPOINT ["dotnet", "BibleBlast.API.dll"]
COPY --from=builder /sln/dist .
