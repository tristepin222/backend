# Vision

## Description

This project is designed to detect labels, and save the results into a database and a bucket and serve an API gateway.

## Getting Started

### Prerequisites

Make sure you have a credentials file with you, or that you have credentials with enough permissions.

from there be sure to follow this https://cloud.google.com/docs/authentication/gcloud.

this will show you how to properly authenticate your app to your google cloud account.

Set up the environment variables with whatever you wish, be careful : if you want to provide your own bucket, be sure it's from google cloud and that you have the following permissions on it : read, write, delete, update on objects.

#### Environment

- IDE (Visual community or Visual Code studio, or anything that can resolve nuget packages and build c# based codes).
- Package manager (Nuget).
- OS (windows 10, not tested under other OS).

#### Libraries

- Google.Apis 1.64.0.
- Google.Cloud.Storage.V1 4.7.0.
- Google.Cloud.Vision.V1 3.3.0.
- MySql.Data 8.2.0 (for DB connection, SQL requests).

### Application configuration

Visual studio community, should automatically download all dependencies, if not just run `nuget update`.

Then you will need to setup your environment variables, under  backend/properties and vision/properties, you'll find a launchsettings.json files, which already have environment variables setup, but you'll need to change them to fit your need.

for example, in backend/propertie/launchsettings.json, at line 27: you have this `"GOOGLE_APPLICATION_CREDENTIALS": "env/es-bi-jessy.json",`, which expects the google application credentials to be present under backend/env/es-bi.jessy.json. 

and under line 28, "BucketName": "csharp.gogle.cld.education", just set whatever your bucket name is.

Under vision/propertie/launchsettings.json, you will also need to change:

    "BucketName": "csharp.gogle.cld.education",
    "DataBaseConnection": "Server=localhost;User=root;Database=vision;Port=3307;Password=root;",
    "Image": "20230727_140005.jpg",
    "PROJECT_ID": "es-bi-370207"
to whatever you need.

### Database configuration

just run the Create_database.sql under the sql folder.

## Deployment

### Dev environment

With visual studio community, you only need to build the application within visual studio and for the tests, it's fairly simple too, just run them.

or you can this command `dotnet run` under any projects you want.

to switch broken, just run `cd ./backend|vision|visionTests`

### Building

We use docker to build images, simply, over visual studio community, build the backend with visual studio community, or run this command  `docker-compose up --build`.

#### Certs error

if you happen to run into issues with certificates, run these commands : `dotnet dev-certs https --clean` and `dotnet dev-certs https --trust`, then restart visual studio commmunity.

## Directory tree
    VisionTest
    ├───Analysers
    ├───Database
    ├───Datas
    ├───Exceptions
    ├───Interfaces
    VisionTestTests

## Collaborate

If you wish to collaborate, simply clone the repo, create a branch with the feature you want to fix, or add.

When you're finish with it, create a pull request, we will review your code, comment it, and it will be merged once it passes our requirements.

For more info please check the wiki.

## License

Unlicensed, Available at project root.

## Contact

If you have any problems, please reach us on github issues.
