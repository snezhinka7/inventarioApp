using InventarioApp.Models;
using InventarioApp.Repositories;
using InventarioApp.Infrastructure;
using InventarioApp.Factories;

namespace InventarioApp.Services;

public class InventarioService
{
    private readonly InMemoryProductoRepo _repository;
    private readonly JsonInventarioStorage _storage;
    private readonly string _rutaInventario;

    public InventarioService(string rutaInventario = "inventario.json")
    {
        _repository = new InMemoryProductoRepo();
        _storage = new JsonInventarioStorage();
        _rutaInventario = rutaInventario;
        CargarInventario();
    }
   
    private void CargarInventario()
    {
        if (_storage.Existe(_rutaInventario))
        {
            var productos = _storage.Cargar(_rutaInventario);
            foreach (var producto in productos)
            {
                _repository.Agregar(producto);
            }
        }
    }

    public void AgregarProducto(string nombre, decimal precio, int cantidad, CategoriaProducto categoria, EstadoProducto estado)
    {
        var producto = ProductoFactory.CrearProducto(nombre, precio, cantidad, categoria, estado);
        _repository.Agregar(producto);
        GuardarInventario();
    }

    public Producto? ObtenerProductoPorId(int id)
    {
        return _repository.ObtenerPorId(id);
    }

    public IEnumerable<Producto> ObtenerTodosLosProductos()
    {
        return _repository.ObtenerTodos();
    }

    public void ActualizarProducto(int id, string nombre, decimal precio, int cantidad, CategoriaProducto categoria, EstadoProducto estado)
    {
        var producto = _repository.ObtenerPorId(id);
        if (producto != null)
        {
            producto.Nombre = nombre;
            producto.Precio = precio;
            producto.Cantidad = cantidad;
            producto.Categoria = categoria;
            producto.Estado = estado;
            _repository.Actualizar(producto);
            GuardarInventario();
        }
    }

    public void EliminarProducto(int id)
    {
        _repository.Eliminar(id);
        GuardarInventario();
    }

    private void GuardarInventario()
    {
        if (_storage.Existe(_rutaInventario))
        {
            _storage.CrearBackup(_rutaInventario);
        }
        var productos = _repository.ObtenerTodos().ToList();
        _storage.Guardar(productos, _rutaInventario);
    }

    //metodos de busqueda
    public IEnumerable<Producto> BuscarPorCategoria(CategoriaProducto categoria)
    {
        return _repository.BuscarPorCategoria(categoria);
    }

    public IEnumerable<Producto> BuscarPorNombre(string nombre)
    {
        return _repository.BuscarPorNombre(nombre);
    }

    public IEnumerable<Producto> ObtenerProductosConStockBajo(int minimo = 5)
    {
        return _repository.ObtenerProductosConStockBajo(minimo);
    }
    
    public decimal ObtenerValorTotalInventario()
    {
        return _repository.ObtenerValorTotalInventario();
    }

    public decimal ObtenerPrecioPromedio()
    {
        return _repository.ObtenerPrecioPromedio();
    }

    public Producto? ObtenerProductoMasCaro()
    {
        return _repository.ObtenerProductoMasCaro();
    }

    //metodos de reportes
    public string GenerarResumen()
    {
        var productos = _repository.ObtenerTodos();
        var generadorReporte = new GeneradorReporte(productos);
        return generadorReporte.GenerarResumen();
    }

    public string GenerarReporteStockBajo(int minimo = 5)
    {
        var productos = _repository.ObtenerTodos();
        var generadorReporte = new GeneradorReporte(productos);
        return generadorReporte.GenerarReporteStockBajo(minimo);
    }
    
    public string GenerarReporteTopProductos(int topN = 5)
    {
        var productos = _repository.ObtenerTodos();
        var generadorReporte = new GeneradorReporte(productos);
        return generadorReporte.GenerarReporteTopProductos(topN);
    }

    public string ExportarCsv()
    {
        var productos = _repository.ObtenerTodos();
        var generadorReporte = new GeneradorReporte(productos);
        return generadorReporte.ExportarCsv();
    }
        
}