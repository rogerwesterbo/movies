FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY src/WebApi/Movies.WebApi.csproj .
RUN dotnet restore Movies.WebApi.csproj
COPY src/WebApi .
RUN dotnet build Movies.WebApi.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Movies.WebApi.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

## Add the wait script to the image
ADD src/wait /app/wait
RUN chmod +x /app/wait

CMD /app/wait && dotnet Movies.WebApi.dll