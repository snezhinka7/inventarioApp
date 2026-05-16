using InventarioApp.Factories;
using InventarioApp.Models;
using InventarioApp.Repositories;
using InventarioApp.Infrastructure;

Console.WriteLine("====================== InventarioApp====================");

var almacenamiento = new JsonInventarioStorage();

var productos = new List<Producto>
{
    new Producto { Id = 1, Nombre = "Laptop", Precio = 999.99m, Cantidad = 10, Categoria = CategoriaProducto.Electronica, Estado = EstadoProducto.Activo },
    new Producto { Id = 2, Nombre = "Camiseta", Precio = 19.99m, Cantidad = 50, Categoria = CategoriaProducto.Ropa, Estado = EstadoProducto.Activo },
    new Producto { Id = 3, Nombre = "Silla de Oficina", Precio = 149.99m, Cantidad = 5, Categoria = CategoriaProducto.Muebles, Estado = EstadoProducto.Inactivo },
};

string ruta = "inventario.json";
almacenamiento.CrearBackup(ruta);
almacenamiento.Guardar(productos, ruta);

Console.WriteLine("Productos guardados correctamente!");

var productosCargados = almacenamiento.Cargar(ruta);

Console.WriteLine("Productos cargados correctamente:");

foreach (var producto in productosCargados)
{
    Console.WriteLine($"ID: {producto.Id}, Nombre: {producto.Nombre}, Precio: {producto.Precio}, Cantidad: {producto.Cantidad}, Categoria: {producto.Categoria}, Estado: {producto.Estado}");
}