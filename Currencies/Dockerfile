# build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Currencies.csproj", "./"]
RUN dotnet restore "Currencies.csproj"

COPY . .
RUN dotnet publish "Currencies.csproj" -c Release -o /app

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Currencies.dll"]