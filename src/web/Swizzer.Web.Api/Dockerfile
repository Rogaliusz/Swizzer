FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# copy everything else and build
COPY . ./
RUN ls
RUN pwd
RUN dotnet restore web/Swizzer.Web.Api/
RUN dotnet publish web/Swizzer.Web.Api -c Release -o out

# build runtime image
FROM mcr.microsoft.com/dotnet/core/runtime:3.0
WORKDIR /app
COPY --from=build-env /app      /out/ ./

ENV ASPNETCORE_URLS http://*:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Swizzer.Web.Api.dll"]