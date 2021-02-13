# estagio 1 - base
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
RUN mkdir /app
WORKDIR /app
EXPOSE 80/tcp
ENV ASPNETCORE_ENVIRONMENT=Development

#estagio 2 - publish
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS publish
WORKDIR /src
COPY . .
WORKDIR /src/StoreBalance.WebApi
RUN dotnet restore
RUN dotnet publish -c Release -o /app/dist

#estagio 3 - final
FROM base AS final
WORKDIR /dist
COPY --from=publish /app/dist .
ENTRYPOINT [ "dotnet", "StoreBalance.WebApi.dll" ]
