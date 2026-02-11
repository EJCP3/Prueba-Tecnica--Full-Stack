# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copiamos todo el contenido del repositorio al contenedor
COPY . .

# 2. Entramos específicamente a la carpeta del proyecto
WORKDIR "/src/InventarioApi"

# 3. Restauramos las dependencias
RUN dotnet restore "InventarioApi.csproj"

# 4. Publicamos el proyecto
RUN dotnet publish "InventarioApi.csproj" -c Release -o /app/publish

# Etapa final de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Asegúrate de que el DLL se llame así
ENTRYPOINT ["dotnet", "InventarioApi.dll"]