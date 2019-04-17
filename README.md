[![CircleCI](https://circleci.com/gh/ppalms/bible-blast/tree/master.svg?style=svg&circle-token=fbd00fc5e5ea12894f335c012f0206df5eeb1090)](https://circleci.com/gh/ppalms/bible-blast/tree/master)
# FUMC Bible Blast
This project consists of a web app running on Angular 7 and .NET Core Web API for tracking kids' progress in the 'Bible Blast' program administered by [First United Methodist Church Tulsa](http://www.fumctulsa.org).

## Environment Variables
In order to run the app, the following environment variables must be set:
- `SQLCONNSTR_DefaultConnection` - a MS SQL Server database connection string
- `SHARED_KEY` - the shared key to use for JWT authentication (a string that will be used to sign and verify tokens)

## Running Locally
From the BibleBlast.API directory, use the `dotnet run` or `dotnet watch run` command to start the Web API project. This solution uses Entity Framework Core; the data schema for the application will be created in the first run and sample data will be seeded in.

From the BibleBlast.SPA directory, use the `npm start` command to run the Angular app.

The app should now be running at http://localhost:4200.

## Building and Running in a Docker Container
To create a docker image, run the following command from the root of the repository:
```
docker build -t <Docker Image Name> .
```

To run the image in a new container that will terminate when stopped:
```
docker run -d --rm=true \
    -e SQLCONNSTR_DefaultConnection='Server=serverlocation;Database=db_name;User ID=db_user;Password=db_password' \
    -e SHARED_KEY='abiglongstring' \
    --name bible-blast <Docker Image Name>
```
