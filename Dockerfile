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
COPY ./BibleBlast.SPA/package.json ./package.json
WORKDIR /sln/BibleBlast.SPA

RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_11.x | bash - && \
    apt-get install -y build-essential nodejs && \
    npm install && \
    npm install -g @angular/cli@7.3.1
    
COPY ./BibleBlast.SPA .
RUN ng build --prod

WORKDIR /sln
RUN ls && cd BibleBlast.API && ls && cd wwwroot && ls
#COPY BibleBlast.API/wwwroot dist/wwwroot

# Copy everything to /app and set entrypoint
#FROM microsoft/dotnet:2.2-aspnetcore-runtime
#WORKDIR /app
#
#ENV ASPNETCORE_ENVIRONMENT Production
#ENV ASPNETCORE_URLS http://0.0.0.0:8080
#EXPOSE 8080
#ENTRYPOINT ["dotnet", "BibleBlast.API.dll"]
#COPY --from=builder /sln/dist .
