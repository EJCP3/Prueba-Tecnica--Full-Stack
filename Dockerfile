# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["InventarioApi/InventarioApi.csproj", "InventarioApi/"]
RUN dotnet restore "InventarioApi/InventarioApi.csproj"
COPY . .
WORKDIR "/src/InventarioApi"
RUN dotnet build "InventarioApi.csproj" -c Release -o /app/build

# Etapa de publicación
FROM build AS publish
RUN dotnet publish "InventarioApi.csproj" -c Release -o /app/publish

# Etapa final (ejecución)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventarioApi.dll"]