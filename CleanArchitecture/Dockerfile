FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ACTIVE_CONNECTION_VARIABLE=DockerPgSQLConnection
ENV ACTIVE_SCHEMA_VARIABLE=PgSQLConnection
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

RUN dotnet dev-certs https --trust

# copy all the layers' csproj files into respective folders
COPY ["./Application/Application.csproj", "src/Application/"]
COPY ["./Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["./Domain/Domain.csproj", "src/Domain/"]
COPY ["./Presentation/Presentation.csproj", "src/Presentation.Core/"]
COPY ["./WebApi/WebApi.csproj", "src/WebApi/"]

# run restore over API project - this pulls restore over the dependent projects as well
RUN dotnet restore "src/WebApi/WebApi.csproj"

COPY . .

# run build over the API project
WORKDIR "/src/WebApi/"
RUN dotnet build -c Release -o /app/build

# run publish over the API project
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS runtime
WORKDIR /app

COPY --from=publish /app/publish .
RUN ls -l
ENTRYPOINT [ "dotnet", "WebApi.dll" ]