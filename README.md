<<<<<<< HEAD
# Distribuidora
=======
# Distribuidora Aseo API

Backend desarrollado en ASP.NET Core con arquitectura en capas (Controller → DAO → DbContext), con soporte para SQL Server y Docker.

---

## Tecnologías utilizadas

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (Windows)
- Docker (Mac)
- Swagger

---

##  Requisitos

Opción 1 (con Docker, MAC)
- Docker Desktop instalado

Opción 2 (SQL Server, Windows)
- .NET SDK 8 o superior
- SQL Server instalado localmente

---

## Cómo ejecutar el proyecto

---

## Opción 1: Docker (Mac)

### 1. Levantar contenedores

```bash
docker-compose up -d
```

### 2. Verificar contenedores activos

```bash
docker ps
```

### 3. Ejecutar la API

```bash
dotnet run
```

### 4. Acceder a Swagger

http://localhost:5067/swagger

---

## Opción 2: SQL Server local (Windows)

### 1. Instalar SQL Server

Descarga e instala SQL Server Express

### 2. Crear la base de datos

Crea una base de datos con el nombre: DistribuidoraDB

### 3. Configurar cadena de conexión

Edita `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=DistribuidoraDB;Trusted_Connection=True;"
}
```

### 4. Ejecutar la aplicación

```bash
dotnet run
```

### 5. Abrir Swagger

http://localhost:5067/swagger
>>>>>>> a4eebaf (Proyecto backend funcional con integración completa)
