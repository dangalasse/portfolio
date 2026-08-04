FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && apt-get install -y nodejs
COPY src/Portfolio/Portfolio.csproj src/Portfolio/
RUN dotnet restore src/Portfolio/Portfolio.csproj
COPY src/Portfolio/ src/Portfolio/
WORKDIR /src/src/Portfolio
RUN npm ci && npm run build
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Portfolio.dll"]
