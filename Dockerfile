﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
COPY /app ./
ENTRYPOINT ["dotnet","app.dll","/codenow/config/appsettings.json"]
