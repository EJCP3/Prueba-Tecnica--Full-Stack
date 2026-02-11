# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Buscamos el archivo .csproj de forma recursiva para no fallar por rutas
COPY . .
RUN dotnet restore

# Publicamos el proyecto
RUN dotnet publish -c Release -o /app/publish

# Etapa final de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Asegúrate de que el nombre del .dll sea exactamente este
ENTRYPOINT ["dotnet", "InventarioApi.dll"]