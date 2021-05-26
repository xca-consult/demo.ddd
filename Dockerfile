FROM joined-docker.artifactory.danskenet.net/baseimages/mcr.microsoft.com/dotnet/core/aspnet:3.1-with-db-certs
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
WORKDIR /app
COPY YZ.ITI.Sample.DDD/bin/Release/netcoreapp3.1/publish .
RUN ls .
ENTRYPOINT [ "dotnet", "YZ.ITI.Sample.DDD.dll" ]
