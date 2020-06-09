FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
COPY . /app
WORKDIR /app
RUN dotnet publish -c Release -o publish -r linux-x64 .

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY --from=build-env /app/publish /app
ENTRYPOINT ["dotnet", "/app/multiplication.dll"]
