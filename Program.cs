using InventarioApp.Models;
using InventarioApp.Services;

var servicio = new InventarioService();
bool activo = true;

while (activo)
{
    MostrarMenu();
    string opcion = Console.ReadLine() ?? string.Empty;

    switch (opcion)
    {
        case "1":
            AgregarProducto();
            break;
        case "2":
            ListarProductos();
            break;
        case "3":
            BuscarPorId();
            break;
        case "4":
            EliminarProducto();
            break;
        case "5":
            BuscarPorCategoria();
            break;
        case "6":
            MostrarResumen();
            break;
        case "7":
            MostrarStockBajo();
            break;
        case "8":
            MostrarEstadisticas();
            break;
        case "9":
            ExportarCsv();
            break;
        case "10":
            activo = false;
            Console.WriteLine("Saliendo de la aplicación...");
            break;
        default:
            Console.WriteLine("Opción no válida. Intente nuevamente.");
            break;
    }

    void MostrarMenu()
    {
        Console.WriteLine("====================== InventarioApp====================");
        Console.WriteLine("\nSeleccione una opción: (numerica)");
        Console.WriteLine("1. Agregar producto");
        Console.WriteLine("2. Listar productos");
        Console.WriteLine("3. Buscar producto por ID");
        Console.WriteLine("4. Eliminar producto");
        Console.WriteLine("5. Buscar productos por categoría");
        Console.WriteLine("6. Mostrar resumen del inventario");
        Console.WriteLine("7. Mostrar productos con stock bajo");
        Console.WriteLine("8. Mostrar estadísticas del inventario");
        Console.WriteLine("9. Exportar inventario a CSV");
        Console.WriteLine("10. Salir");
    }

    bool TryLeerCategoria(string entrada, out CategoriaProducto categoria)
    {
        return Enum.TryParse(entrada, true, out categoria)
               && Enum.IsDefined(typeof(CategoriaProducto), categoria);
    }

    void AgregarProducto()
    {
        Console.WriteLine("Ingrese el nombre del producto:");
        string nombre = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Ingrese el precio del producto:");
        decimal precio = decimal.TryParse(Console.ReadLine(), out var p) ? p : 0;

        Console.WriteLine("Ingrese la cantidad del producto:");
        int cantidad = int.TryParse(Console.ReadLine(), out var c) ? c : 0;

        Console.WriteLine("Seleccione la categoría del producto:");
        foreach (var cat in Enum.GetValues<CategoriaProducto>())
        {
            Console.WriteLine($"{(int)cat}. {cat}");
        }
        CategoriaProducto categoria;
        while (true)
        {
            Console.Write("Ingrese número o nombre de categoría: ");
            string entradaCategoria = Console.ReadLine() ?? string.Empty;

            bool categoriaValida = TryLeerCategoria(entradaCategoria, out CategoriaProducto catSel);

            if (categoriaValida)
            {
                categoria = catSel;
                break;
            }

            Console.WriteLine("Categoría inválida. Intente nuevamente con una opción de la lista.");
        }

        Console.WriteLine("Seleccione el estado del producto:");
        foreach (var est in Enum.GetValues<EstadoProducto>())
        {
            Console.WriteLine($"{(int)est}. {est}");
        }
        EstadoProducto estado = Enum.TryParse(Console.ReadLine(), out EstadoProducto estSel) ? estSel : EstadoProducto.Activo;

        servicio.AgregarProducto(nombre, precio, cantidad, categoria, estado);
        Console.WriteLine("Producto agregado exitosamente.");
    }

    void ListarProductos()
    {
        var productos = servicio.ObtenerTodosLosProductos();
        if (!productos.Any())
        {
            Console.WriteLine("No hay productos en el inventario.");
            return;
        }
        Console.WriteLine("Productos en el inventario:");
        foreach (var producto in productos)
        {
            Console.WriteLine($"ID: {producto.Id}, Nombre: {producto.Nombre}, Precio: {producto.Precio}, Cantidad: {producto.Cantidad}, Categoría: {producto.Categoria}, Estado: {producto.Estado}");
        }
    }

    void BuscarPorId()
    {
        Console.WriteLine("Ingrese el ID del producto a buscar:");
        int id = int.TryParse(Console.ReadLine(), out var i) ? i : 0;
        var producto = servicio.ObtenerProductoPorId(id);
        if (producto != null)
        {
            Console.WriteLine($"ID: {producto.Id}, Nombre: {producto.Nombre}, Precio: {producto.Precio}, Cantidad: {producto.Cantidad}, Categoría: {producto.Categoria}, Estado: {producto.Estado}");
        }
        else
        {
            Console.WriteLine("Producto no encontrado.");
        }
    }

    void EliminarProducto()
    {
        Console.WriteLine("Ingrese el ID del producto a eliminar:");
        int id = int.TryParse(Console.ReadLine(), out var i) ? i : 0;
        var producto = servicio.ObtenerProductoPorId(id);
        if (producto == null)
        {
            Console.WriteLine("Producto no encontrado.");
            return;
        }
        servicio.EliminarProducto(id);
        Console.WriteLine("Producto eliminado exitosamente.");
    }

    void BuscarPorCategoria()
    {
        Console.WriteLine("Seleccione la categoría a buscar:");
        foreach (var cat in Enum.GetValues<CategoriaProducto>())
        {
            Console.WriteLine($"{(int)cat}. {cat}");
        }
        string entradaCategoria = Console.ReadLine() ?? string.Empty;
        if (!TryLeerCategoria(entradaCategoria, out CategoriaProducto categoria))
        {
            Console.WriteLine("Categoría inválida. Intente nuevamente.");
            return;
        }
        var productos = servicio.BuscarPorCategoria(categoria);
        if (!productos.Any())
        {
            Console.WriteLine("No se encontraron productos en esa categoría.");
            return;
        }
        Console.WriteLine($"Productos en la categoría {categoria}:");
        foreach (var producto in productos)
        {
            Console.WriteLine($"ID: {producto.Id}, Nombre: {producto.Nombre}, Precio: {producto.Precio}, Cantidad: {producto.Cantidad}, Estado: {producto.Estado}");
        }
    }

    void MostrarResumen()
    {
        var resumen = servicio.GenerarResumen();
        Console.WriteLine(resumen);
    }

    void MostrarStockBajo()
    {
        var reporte = servicio.GenerarReporteStockBajo();
        Console.WriteLine(reporte);
    }

    void MostrarEstadisticas()
    {
        Console.WriteLine("------------ ESTADISTICAS DEL INVENTARIO ------------");
        Console.WriteLine($"Valor total del inventario: {servicio.ObtenerValorTotalInventario()}");
        Console.WriteLine($"Precio promedio de los productos: {servicio.ObtenerPrecioPromedio():N2}");

        var productoMasCaro = servicio.ObtenerProductoMasCaro();
        if (productoMasCaro != null)        {
            Console.WriteLine($"Producto más caro: {productoMasCaro.Nombre}, Precio: {productoMasCaro.Precio})");
        }
        else
        {
            Console.WriteLine("No hay productos en el inventario para mostrar el producto más caro.");
        }
    }

    void ExportarCsv()
    {
        string csv = servicio.ExportarCsv();
        Console.WriteLine(csv);
    }

}



    