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
RUN apt-get update \
  && apt-get install -y --no-install-recommends curl \
  && rm -rf /var/lib/apt/lists/*
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
HEALTHCHECK --interval=30s --timeout=5s --start-period=20s --retries=3 \
  CMD curl -fsS http://127.0.0.1:8080/api/status || exit 1
ENTRYPOINT ["dotnet", "Portfolio.dll"]
