using System.Text.Json;
using System.Text.Json.Serialization;
using InventarioApp.Models;

namespace InventarioApp.Infrastructure;

public class JsonInventarioStorage
{
    private readonly Filemanager _fileManager = new();
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    public void Guardar(List<Producto> productos, string ruta)
    {
        string json = JsonSerializer.Serialize(productos, _options);
        _fileManager.Escribir(ruta, json);
    }

    public List<Producto> Cargar(string ruta)
    {
        string json = _fileManager.Leer(ruta);
        return JsonSerializer.Deserialize<List<Producto>>(json, _options) ?? new List<Producto>();
    }

    public string CrearBackup(string ruta)
    {
        if (!_fileManager.Existe(ruta))
        {
            throw new FileNotFoundException($"El archivo '{ruta}' no existe para crear un backup.");
        }
        string? directorio = Path.GetDirectoryName(ruta);
        string nombreArchivo = Path.GetFileNameWithoutExtension(ruta);
        string extension = Path.GetExtension(ruta);
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string backupRuta = Path.Combine(directorio ?? ".", $"{nombreArchivo}_backup_{timestamp}{extension}");
        File.Copy(ruta, backupRuta);
        return backupRuta;
    }
}