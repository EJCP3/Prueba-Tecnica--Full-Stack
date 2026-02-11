# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copiamos TODO el contenido del repo
COPY . .

# 2. Entramos a la subcarpeta donde está el archivo de proyecto real
# Ajustado para entrar en la doble carpeta
WORKDIR "/src/InventarioApi/InventarioApi"

# 3. Restauramos usando el archivo que está en esta carpeta
RUN dotnet restore

# 4. Publicamos el proyecto
RUN dotnet publish -c Release -o /app/publish

# Etapa final de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Asegúrate de que el nombre del .dll sea correcto
ENTRYPOINT ["dotnet", "InventarioApi.dll"]