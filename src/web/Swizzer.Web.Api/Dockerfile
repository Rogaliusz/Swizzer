FROM microsoft/dotnet:3.0-sdk AS build-env
WORKDIR /app

# copy everything else and build
COPY . ./
RUN ls
RUN pwd
RUN dotnet restore
RUN dotnet publish web/Swizzer.Web.Api -c Release -o out

# build runtime image
FROM microsoft/dotnet:3.0-runtime 
WORKDIR /app
COPY --from=build-env /app/src/web/Swizzer.Web.Api/out/ ./

ENV ASPNETCORE_URLS http://*:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Swizzer.Web.Api.dll"]