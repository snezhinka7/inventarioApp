using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using InventarioApp.Models;

namespace InventarioApp.Infrastructure;

public class GeneradorReporte
{
    private readonly IEnumerable<Producto> _productos;

    public GeneradorReporte(IEnumerable<Producto> productos)
    {
        _productos = productos;
    }

    public string GenerarResumen()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Reporte de Inventario");
        sb.AppendLine($"Total de productos: {_productos.Count()}");
        sb.AppendLine($"Valor total del inventario: {_productos.Sum(p => p.ValorTotal):N2}");

        var productosPorCategoria = _productos.GroupBy(p => p.Categoria)
            .Select(g => new { Categoria = g.Key, Cantidad = g.Count()});

        sb.AppendLine("Productos por categoría: ");
        foreach (var categoria in productosPorCategoria)
        {
            sb.AppendLine($"{categoria.Categoria}: {categoria.Cantidad}");
        }
        return sb.ToString();
    }

    public string GenerarReporteStockBajo(int umbral)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Productos con stock menor a {umbral}:");
        var productosBajoStock = _productos.Where(p => p.Cantidad < umbral)
            .OrderBy(p => p.Cantidad);
        if (!productosBajoStock.Any())
        {
            sb.AppendLine("No hay productos con stock bajo.");
            return sb.ToString();
        }
        foreach (var producto in productosBajoStock)
        {
            sb.AppendLine($"ID: {producto.Id}, Nombre: {producto.Nombre}, Cantidad: {producto.Cantidad}, precio: {producto.Precio:N2}");
        }
        return sb.ToString();
    }

    public string GenerarReporteTopProductos(int topN)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Top {topN} productos por valor total en inventario:");
        var topProductos = _productos.OrderByDescending(p => p.ValorTotal).Take(topN);
        if (!topProductos.Any())
        {
            sb.AppendLine("No hay productos en el inventario.");
            return sb.ToString();
        }

        int posicion = 1;
        foreach (var producto in topProductos)
        {
            sb.AppendLine($"{posicion}. ID: {producto.Id}, Nombre: {producto.Nombre}, Valor Total: {producto.ValorTotal:N2}");
            posicion++;
        }
        return sb.ToString();
    }

    public string ExportarCsv()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Id,Nombre,Precio,Cantidad,Categoria,Estado,ValorTotal");
        foreach (var producto in _productos.OrderBy(p => p.Id))
        {
            sb.AppendLine($"{producto.Id},{producto.Nombre},{producto.Precio},{producto.Cantidad},{producto.Categoria},{producto.Estado},{producto.ValorTotal}");
        }
        return sb.ToString();
    }

    public string ExportarJson()
    {
        var resumen = new
        {
            TotalProductos = _productos.Count(),
            ValorTotalInventario = _productos.Sum(p => p.ValorTotal),
            ProductosPorCategoria = _productos.GroupBy(p => p.Categoria)
                .Select(g => new { Categoria = g.Key, Cantidad = g.Count() }),
            TopProductos = _productos.OrderByDescending(p => p.ValorTotal).Take(5)
                .Select(p => new { p.Id, p.Nombre, p.Cantidad, p.ValorTotal })
        };
        
        return JsonSerializer.Serialize(resumen, new JsonSerializerOptions { WriteIndented = true });
    }

    
}