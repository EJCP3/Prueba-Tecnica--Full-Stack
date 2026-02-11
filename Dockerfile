# Etapa de compilación con SDK 9.0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .

# Entramos a la subcarpeta detectada
WORKDIR "/src/InventarioApi/InventarioApi"

RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Etapa final con ASP.NET 9.0
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "InventarioApi.dll"]