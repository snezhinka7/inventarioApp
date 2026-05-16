using InventarioApp.Factories;
using InventarioApp.Models;
using InventarioApp.Repositories;
using InventarioApp.Infrastructure;

Console.WriteLine("====================== InventarioApp====================");

var productos = new List<Producto>
{
    new Producto { Id = 1, Nombre = "Laptop", Precio = 999.99m, Cantidad = 10, Categoria = CategoriaProducto.Electronica, Estado = EstadoProducto.Activo },
    new Producto { Id = 2, Nombre = "Camiseta", Precio = 19.99m, Cantidad = 50, Categoria = CategoriaProducto.Ropa, Estado = EstadoProducto.Activo },
    new Producto { Id = 3, Nombre = "Silla de Oficina", Precio = 149.99m, Cantidad = 5, Categoria = CategoriaProducto.Muebles, Estado = EstadoProducto.Inactivo },
    new Producto { Id = 4, Nombre = "Smartphone", Precio = 499.99m, Cantidad = 20, Categoria = CategoriaProducto.Electronica, Estado = EstadoProducto.Activo },
    new Producto { Id = 5, Nombre = "Pantalones", Precio = 39.99m, Cantidad = 30, Categoria = CategoriaProducto.Ropa, Estado = EstadoProducto.Activo },
    new Producto { Id = 6, Nombre = "Mesa de Comedor", Precio = 299.99m, Cantidad = 3, Categoria = CategoriaProducto.Muebles, Estado = EstadoProducto.Inactivo },
    new Producto { Id = 7, Nombre = "Auriculares", Precio = 89.99m, Cantidad = 15, Categoria = CategoriaProducto.Electronica, Estado = EstadoProducto.Activo },
    new Producto { Id = 8, Nombre = "Chaqueta", Precio = 59.99m, Cantidad = 25, Categoria = CategoriaProducto.Ropa, Estado = EstadoProducto.Activo }
};

var generadorReporte = new GeneradorReporte(productos);
Console.WriteLine(generadorReporte.GenerarResumen());
Console.WriteLine("====================== Reporte de Stock Bajo ====================");
Console.WriteLine(generadorReporte.GenerarReporteStockBajo(10));
Console.WriteLine("====================== Top Productos ====================");
Console.WriteLine(generadorReporte.GenerarReporteTopProductos(5));
Console.WriteLine("====================== Exportar CSV ====================");
Console.WriteLine(generadorReporte.ExportarCsv());
Console.WriteLine("====================== Exportar JSON ====================");
Console.WriteLine(generadorReporte.ExportarJson());