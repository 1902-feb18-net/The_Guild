FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as build

WORKDIR /app

COPY ./*.csproj ./
RUN dotnet restore

COPY . ./

RUN dotnet build --no-restore

RUN dotnet test

RUN dotnet publish --no-restore -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime

WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT [ "dotnet", "The_Guild.WebApp.dll" ]
