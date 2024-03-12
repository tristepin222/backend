FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy everything

COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App

COPY es-bi-jessy.json ./es-bi-jessy.json
COPY 20230727_140005.jpg ./20230727_140005.jpg
ENV PROJECT_ID=es-bi-370207
ENV Image=20230727_140005.jpg
ENV BucketName=csharp.gogle.cld.education


COPY --from=build-env /App/out .

ENTRYPOINT ["dotnet", "Vision.dll"]