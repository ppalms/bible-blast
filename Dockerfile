# Build Web API project
FROM microsoft/dotnet:2.2-sdk AS builder
WORKDIR /sln

COPY ./BibleBlast.API/BibleBlast.API.csproj ./BibleBlast.API/BibleBlast.API.csproj
RUN dotnet restore "./BibleBlast.API/BibleBlast.API.csproj"
COPY . .
WORKDIR /sln/BibleBlast.API
RUN dotnet build -c Release --no-restore
RUN dotnet publish -c Release -o "../dist" --no-restore

# Build Angular app
FROM node:10.15.3
RUN mkdir ../BibleBlast.SPA
WORKDIR /sln/BibleBlast.SPA

COPY ./BibleBlast.SPA/package.json ./package.json
RUN npm install
RUN npm install -g @angular/cli@7.3.1
COPY ./BibleBlast.SPA .
RUN ng build --prod

# Copy everything to /app and run
FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://0.0.0.0:8080
ENTRYPOINT ["dotnet", "BibleBlast.API.dll"]
COPY --from=builder /sln/dist .
