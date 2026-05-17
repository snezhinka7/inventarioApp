# Sistema de Gestion de Inventario

Aplicacion de consola en C# para gestionar productos de inventario, con persistencia en JSON, backups automaticos y reportes.

## Objetivo

Practicar fundamentos de .NET y C# aplicando:

- Modelado de dominio (Producto, Categoria, Estado).
- Arquitectura por capas simple (Program, Services, Repositories, Infrastructure, Factories).
- Persistencia en archivos JSON.
- Consultas con LINQ.
- Validacion de entrada en consola.

## Funcionalidades

- Agregar productos.
- Listar productos.
- Buscar por ID.
- Eliminar productos.
- Buscar por categoria.
- Mostrar resumen del inventario.
- Mostrar reporte de stock bajo.
- Mostrar estadisticas (valor total, promedio, producto mas caro).
- Exportar inventario en formato CSV (texto en consola).

## Menu actual

La aplicacion muestra este menu principal:

1. Agregar producto
2. Listar productos
3. Buscar producto por ID
4. Eliminar producto
5. Buscar productos por categoria
6. Mostrar resumen del inventario
7. Mostrar productos con stock bajo
8. Mostrar estadisticas del inventario
9. Exportar inventario a CSV
10. Salir

## Validaciones importantes

- Categoria: acepta numero o nombre del enum y no distingue mayusculas/minusculas.
- Categoria: solo permite valores definidos (evita enums "fantasma" como 99).
- Precio y cantidad: la factoria valida que no sean negativos.
- Nombre: no puede estar vacio o en blanco.

## Persistencia y backups

- El inventario se guarda en `inventario.json`.
- Al iniciar, si existe el archivo, se carga automaticamente.
- Antes de guardar cambios, se crea un backup cuando el archivo ya existe.
- El backup se nombra con timestamp, por ejemplo:
	- `inventario_backup_2026-05-16_14-35-10.json`

## Tecnologias

- .NET 10 (`net10.0`)
- C#
- `System.Text.Json`
- LINQ

## Requisitos

- SDK de .NET 10 instalado.

Verificar instalacion:

```bash
dotnet --version
```

## Ejecucion

Desde la carpeta raiz del proyecto:

```bash
dotnet restore
dotnet build
dotnet run
```

## Estructura del proyecto

```text
apuntes csharp/
|-- Program.cs
|-- apuntes csharp.csproj
|-- src/
|   |-- Factories/
|   |   `-- ProductoFactory.cs
|   |-- Infrastructure/
|   |   |-- Filemanager.cs
|   |   |-- GeneradorReporte.cs
|   |   `-- JsonInventarioStorage.cs
|   |-- Models/
|   |   |-- CategoriaProductos.cs
|   |   |-- EstadoProducto.cs
|   |   |-- Producto.cs
|   |   `-- Proveedor.cs
|   |-- Repositories/
|   |   |-- IProductoRepository.cs
|   |   `-- InMemoryProductoRepo.cs
|   `-- Services/
|       `-- InventarioService.cs
`-- README.md
```

## Ejemplo rapido de uso

1. Ejecutar la app con `dotnet run`.
2. Elegir opcion 1 para agregar productos.
3. Elegir opcion 2 para listarlos.
4. Elegir opcion 6 u 8 para ver resumen y estadisticas.
5. Elegir opcion 9 para exportar CSV en consola.

## Mejoras futuras sugeridas

- Editar productos desde el menu.
- Guardar la exportacion CSV en archivo.
- Agregar pruebas unitarias.
- Implementar repositorio persistente adicional (por ejemplo SQLite).
