using InventarioApp.Factories;
using InventarioApp.Models;
using InventarioApp.Repositories;

Console.WriteLine("====================== InventarioApp====================");

var repositorio = new InMemoryProductoRepo();

Producto laptop = ProductoFactory.CrearProducto(nombre: "Laptop Dell XPS 13", precio: 1200, cantidad: 5, CategoriaProducto.Electronica);
Producto mouse = ProductoFactory.CrearProducto(nombre: "Mouse Logitech MX Master", precio: 99, cantidad: 20, CategoriaProducto.Electronica);
Producto teclado = ProductoFactory.CrearProducto(nombre: "Teclado Mecánico", precio: 150, cantidad: 3, CategoriaProducto.Electronica);
Producto silla = ProductoFactory.CrearProducto(nombre: "Silla Ergonómica Herman Miller", precio: 500, cantidad: 8, CategoriaProducto.Muebles);
Producto escritorio = ProductoFactory.CrearProducto(nombre: "Escritorio Stand-up", precio: 300, cantidad: 2, CategoriaProducto.Muebles);

repositorio.Agregar(laptop);
repositorio.Agregar(mouse);
repositorio.Agregar(teclado);
repositorio.Agregar(silla);
repositorio.Agregar(escritorio);

Console.WriteLine($"Productos agregados: {repositorio.Cantidad}");

//consulta basicas por LINQ

IEnumerable<Producto> electronicos = repositorio.BuscarPorCategoria(CategoriaProducto.Electronica);
Console.WriteLine($"Productos electronicos: {electronicos.Count()}");

foreach (Producto producto in electronicos)
{
    Console.WriteLine($" {producto.Nombre} : {producto.Precio:C2}");
}

IEnumerable<Producto> conMouse = repositorio.BuscarPorNombre("mouse");
Console.WriteLine($"\nProductos con mouse: {conMouse.Count()}");

foreach (Producto producto in conMouse)
{
    Console.WriteLine($" {producto.Nombre} : {producto.Precio:C2}");
}

IEnumerable<string> nombres = repositorio.ObtenerNombres();
Console.WriteLine($"\nTodos los nombres de los productos: {string.Join(", ", nombres)}");

bool hayStockBajo = repositorio.HayStockBajo();
Console.WriteLine($"\nHay stock bajo?: {hayStockBajo}");