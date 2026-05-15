namespace InventarioApp.Factories;

using InventarioApp.Models;
public static class ProductoFactory
{
    public static int _nextId = 1;
    public static Producto CrearProducto(string nombre, decimal precio, int cantidad, CategoriaProducto categoria = CategoriaProducto.Otros)
    {
        //Validaciones tempranas o fail fast - Guard Clauses
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del producto no puede estar vacío.", nameof(nombre));
        if (precio < 0)
            throw new ArgumentOutOfRangeException(nameof(precio), "El precio del producto no puede ser negativo.");
        if (cantidad < 0)
            throw new ArgumentOutOfRangeException(nameof(cantidad), "La cantidad del producto no puede ser negativa.");

        return new Producto
        {
            Id = _nextId++,
            Nombre = nombre,
            Precio = precio,
            Cantidad = cantidad,
            Categoria = categoria,
            FechaRegistro = DateTime.Now,
            Estado = EstadoProducto.Activo
        };
    }

    public static Producto CrearConStock(string nombre, decimal precio, int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentOutOfRangeException(nameof(cantidad), "La cantidad debe ser mayor a cero para crear un producto con stock.");
        return CrearProducto(nombre, precio, cantidad);
    }
}