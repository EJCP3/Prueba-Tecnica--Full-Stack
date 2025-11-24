
# Prueba Técnica Full-Stack

📌 Descripción del Proyecto

Este proyecto consiste en una aplicación Full-Stack diseñada para gestionar productos, clientes y ventas. La solución incluye un backend en .NET con una API REST  y un frontend en Vue.js, aplicando una arquitectura en capas, principios SOLID y con un código organizado.

🧩 Características Principales
# Features

 ### Backend (.NET)

- API REST con CRUD de Productos, Clientes y Ventas
- Arquitectura en Capas: Controllers, Custom, Middlewares, Migrations, Models, DTOs
- Entity Framework Core con Migrations
- SQL Server como base de datos
- Autenticación por JWT (tokens con roles)
- Manejo de errores centralizado
- Pruebas Unitarias

### Fronted (Vue.js)

- Vue 3 + Composition API
- Vite como bundler
- Pinia para manejo de estado
- Axios para consumir la API
- Validación visual de formularios
- Diseño responsivo
- Componentes reutilizables

# 🚀 Tecnologías Utilizadas

### Frontend

- Vue.js
- Vue Router
- TailwindCSS 
- DaisyUI
- Pinia
- Formkit

### Backend

- .NET 10
-  C#
- Entity Framework Core
- SQL Server
- JWT
- Swagger
- xUnit
# Arquitectura

## Fronted 

```

/SRC
 ├── Components
 ├── Composable
 ├── Layouts
 ├── Logic
 ├── Pages
 ├── Router
 ├── Stores
 └── Schema



```

## Backend 

```
/API
 ├── Controllers
 ├── Custom
 ├── Exceptions
 ├── Middlewares
 ├── Migrations
 ├── Models
 └── Context

```
## 📚 Entidades Principales


### 🟦 Productos

| Campo      | Tipo           | Comentario                        |
|------------|----------------|----------------------------------|
| ID         | int            | Clave primaria, autoincremental  |
| Nombre     | nvarchar(max)  | Nombre del producto              |
| Descripción| nvarchar(max)  | Detalle del producto             |
| Precio     | decimal(18,2)  | Precio unitario                  |
| Stock      | int            | Cantidad disponible              |



### 🟩 Clientes

| Campo    | Tipo          | Comentario                      |
| -------- | ------------- | ------------------------------- |
| Id       | int           | Clave primaria, autoincremental |
| Nombre   | nvarchar(max) | Nombre del cliente              |
| Email    | nvarchar(max) | Correo electrónico              |
| Telefono | nvarchar(max) | Número de teléfono              |

### 🟧 Usuarios

| Campo         | Tipo          | Comentario                        |
| ------------- | ------------- | --------------------------------- |
| ID            | int           | Clave primaria, autoincremental   |
| Nombre        | nvarchar(max) | Nombre completo                   |
| Email         | nvarchar(max) | Correo electrónico                |
| Username      | nvarchar(max) | Nombre de usuario                 |
| PasswordHash  | nvarchar(max) | Contraseña encriptada             |
| Rol           | nvarchar(max) | Rol del usuario (`admin`, `user`) |
| FechaCreacion | datetime2     | Fecha de creación del registro    |



### 🟥 Ventas

| Campo     | Tipo          | Comentario                      |
| --------- | ------------- | ------------------------------- |
| ID        | int           | Clave primaria, autoincremental |
| Fecha     | datetime2     | Fecha de la venta               |
| Total     | decimal(18,2) | Monto total de la venta         |
| ClienteID | int           | Clave foránea a Clientes        |

**Relaciones:**

**ClienteID → Clientes(Id)**

### 🟪 VentaDetalles

| Campo          | Tipo          | Comentario                                 |
| -------------- | ------------- | ------------------------------------------ |
| ID             | int           | Clave primaria, autoincremental            |
| VentaID        | int           | Clave foránea a Ventas                     |
| ProductoID     | int           | Clave foránea a Productos                  |
| Cantidad       | int           | Cantidad del producto en la venta          |
| Precio         | decimal(18,2) | Precio unitario del producto               |
| NombreProducto | nvarchar(max) | Nombre del producto (copia para histórico) |


**Relaciones:**

**VentaID → Ventas(ID)**

**ProductoID → Productos(ID)**


# Roles y Permisos

### Admin

-  CRUD de productos
-  CRUD de clientes
-  Registrar ventas
-  Ver historial de ventas
-  Agregar nuevo usuarios

### User

- Crear y ver productos
- Crear y ver clientes
- Registrar ventas
- Ver historial de ventas



# 🚀 Instrucciones para Ejecutar la Aplicación


##  Backend (.NET)


```bash
  dotnet restore
```

#### Configurar cadena de conexión

En appsettings.json:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=InventarioDB;Trusted_Connection=True;"
}

```

Ejecutar migraciones

```bash
  dotnet ef database update
```

Levantar el servidor

```bash
  dotnet run
```

**API disponible en:**
```bash
 https://localhost:7249/api
```
##  Frontend (Vue.js)

#### Instalar dependencias

```bash
  pnpm install
```

#### Configurar URL del backend

En /src/axios.js:

```js
export const api = axios.create({
  baseURL: "https://localhost:7249/api",
});

```

Ejecutar aplicación

```bash
  pnpm run dev
```

**API disponible en:**
```bash
 http://localhost:5173
```