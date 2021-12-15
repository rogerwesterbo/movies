FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApi/Movies.WebApi.csproj", "WebApi/"]
RUN dotnet restore "WebApi/Movies.WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "Movies.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Movies.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Movies.WebApi.dll"]