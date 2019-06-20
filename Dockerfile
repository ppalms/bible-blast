# Build Angular app
FROM node:10.15.3 AS clientbuild
WORKDIR /sln/BibleBlast.SPA
COPY ./BibleBlast.SPA .

RUN npm install
RUN ng build --prod

# Build Web API project
FROM microsoft/dotnet:2.2-sdk AS apibuild
WORKDIR /sln/BibleBlast.API
COPY ./BibleBlast.API/BibleBlast.API.csproj .

RUN dotnet restore
COPY ./BibleBlast.API .

RUN dotnet build -c Release --no-restore
RUN dotnet publish -c Release -o "../dist" --no-restore

# Copy Angular app to .NET root
COPY --from=clientbuild /sln/dist/wwwroot /sln/dist/wwwroot

# Copy everything to /app and set entrypoint
FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://0.0.0.0:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "BibleBlast.API.dll"]
COPY --from=apibuild /sln/dist .
