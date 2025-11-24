# Backend 

📌 Descripción del Backend

el backend está construido con .NET y diseñado para manejar toda la lógica del sistema de inventario, clientes y ventas. Incluye controladores para cada entidad principal, DTOs para enviar y recibir datos de manera segura, validaciones, manejo centralizado de errores y migraciones para administrar la base de datos en SQL Server.


# Endpoints de la API

- 🔐 Acceso (Autenticación)

| Método | Endpoint                  | Descripción                  |
| ------ | ------------------------- | ---------------------------- |
| POST   | `/api/Acceso/Registrarse` | Registrar un nuevo usuario   |
| POST   | `/api/Acceso/Login`       | Iniciar sesión y obtener JWT |

- 👤 Clientes

| Método | Endpoint             | Descripción                 |
| ------ | -------------------- | --------------------------- |
| GET    | `/api/Clientes`      | Obtener listado de clientes |
| POST   | `/api/Clientes`      | Crear un cliente            |
| GET    | `/api/Clientes/{id}` | Obtener un cliente por ID   |
| PUT    | `/api/Clientes/{id}` | Actualizar un cliente       |
| DELETE | `/api/Clientes/{id}` | Eliminar un cliente         |

- 📦 Productos

| Método | Endpoint              | Descripción             |
| ------ | --------------------- | ----------------------- |
| GET    | `/api/Productos`      | Listar productos        |
| POST   | `/api/Productos`      | Crear un producto       |
| GET    | `/api/Productos/{id}` | Obtener producto por ID |
| PUT    | `/api/Productos/{id}` | Actualizar producto     |
| DELETE | `/api/Productos/{id}` | Eliminar producto       |

- 🧑‍💼 Usuarios

| Método | Endpoint              | Descripción             |
| ------ | --------------------- | ----------------------- |
| GET    | `/api/Usuarios`      | Obtener todos los usuarios      |
| POST   | `/api/Usuarios`      | Crear usuario      |
| GET    | `/api/Usuarios/{id}` | Obtener usuarios por ID |
| PUT    | `/api/Usuarios/{id}` | Actualizar usuarios     |
| DELETE | `/api/Usuarios/{id}` | Eliminar usuarios       |

- 🧾 VentaDetalle

| Método | Endpoint              | Descripción             |
| ------ | --------------------- | ----------------------- |
| GET    | `/api/VentaDetalle`      | Obtener todos los venta      |
| POST   | `/api/VentaDetalle`      | Crear venta      |
| GET    | `/api/VentaDetalle/{id}` | Obtener venta por ID |
| PUT    | `/api/VentaDetalle/{id}` | Actualizar venta     |
| DELETE | `/api/VentaDetalle/{id}` | Eliminar venta       |


- 🧾 Usuarios

| Método | Endpoint              | Descripción             |
| ------ | --------------------- | ----------------------- |
| GET    | `/api/Ventas`      | Obtener todos los Ventas      |
| POST   | `/api/Ventas`      | Crear Ventas      |
| GET    | `/api/Ventas/{id}` | Obtener Ventas por ID |
| PUT    | `/api/Ventas/{id}` | Actualizar Ventas     |
| DELETE | `/api/Ventas/{id}` | Eliminar Ventas       |
