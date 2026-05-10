using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;

MostrarBanner();

if (args.Length > 0)
{
    var command = args[0].ToLower();

    switch (command)
    {
        case "--help":
        case "-h":
            MostrarAyuda();
            Environment.Exit(0);
            break;

        case "--version":
        case "-v":
            Console.WriteLine($"Version: {version}");
            Environment.Exit(0);
            break;

        default:
            Console.WriteLine($"Comando desconocido: {command}");
            Console.WriteLine("Usa --help para ver los comandos disponibles.");
            Environment.Exit(2);
            break;
    }
}

int cantidadProductos = 0;
decimal valorTotal = 0.00m;
bool sistemaActivo = true;
string nombreSistema = "Sistema de Gestión de Inventario";
decimal precio = 0.00m;

Console.WriteLine($"Bienvenido al {nombreSistema}!");
Console.WriteLine($"Productos registrados: {cantidadProductos}");
Console.WriteLine($"Valor total del inventario: {valorTotal:N2}");
Console.WriteLine($"Sistema activo: {(sistemaActivo ? "Sí" : "No")}");

Console.WriteLine("Ingrese una cantidad: ");
string? entradaCantidad = Console.ReadLine();

//conversion segura TryParse
if (int.TryParse(entradaCantidad, out int cantidad))
{
    Console.WriteLine($"Cantidad ingresada: {cantidad}");
    cantidadProductos = cantidad;
}
else
{
    Console.WriteLine("Entrada no válida para cantidad.");
}

Console.WriteLine("Ingrese un precio: ");
string? entradaPrecio = Console.ReadLine();

//conversion segura TryParse
if (decimal.TryParse(entradaPrecio, out decimal precioIngresado))
{
    Console.WriteLine($"Precio ingresado: {precioIngresado:N2}");
    valorTotal = cantidadProductos * precioIngresado;
    Console.WriteLine($"Valor total actualizado: $ {valorTotal:N2}");
}
else
{
    Console.WriteLine("Entrada no válida para precio.");
}




/*
string? nombre = null;
int longitud = nombre.Length;
Console.WriteLine($"La longitud del nombre es: {longitud}");

// Problema: readline puede devolver null
Console.Write("Ingrese un valor: ");
string? entrada = Console.ReadLine();
int? longitud = entrada?.Length;

// Solucion Operador coalescing ??
//string comando = string.IsNullOrEmpty(entrada) ? "salir" : entrada;
string comandoLimpio = string.IsNullOrWhiteSpace(entrada) ? "salir" : entrada.Trim().ToLower();
Console.WriteLine($"Longitud: {longitud ?? 0}");
Console.WriteLine($"Comando: {comandoLimpio}");
*/

// Loop de nullabilidad
Console.WriteLine("Comandos: listar, agregar, buscar, salir");
Console.WriteLine();

while (sistemaActivo)
{
    Console.Write("inventario: ");
    string? entrada = Console.ReadLine();

    string comando = string.IsNullOrWhiteSpace(entrada) ? "salir" : entrada.Trim().ToLower();
    switch (comando)
    {
        case "salir":
            Console.WriteLine("Saliendo del programa...");
            sistemaActivo = false;
            break;
        case "listar":
            Console.WriteLine($"Lista de productos: {cantidadProductos}");
            break;
        case "":
            break;
        default:
            Console.WriteLine($"Error: comando desconocido '{comando}'");
            Console.WriteLine("Comandos disponibles: listar, agregar, buscar, salir");
            break;
    }
}

/*
Console.Write("Ingrese un comando o ingrese salir para terminar: ");
string? entrada = Console.ReadLine();

if (string.IsNullOrWhiteSpace(entrada) || entrada.ToLower() == "salir")
{
    Console.WriteLine("Saliendo del programa...");
    Environment.Exit(0);
}*/

//FUNCIONES---------------------------------------------------------------------------------------------------------------------------------------
void MostrarAyuda()
{
    Console.WriteLine("USO: InventarioApp [comando] [opciones]");
    Console.WriteLine();
    Console.WriteLine("COMANDOS:");
    Console.WriteLine("  --help, -h      Muestra esta ayuda");
    Console.WriteLine("  --version, -v   Muestra la version del programa");
    Console.WriteLine();
    Console.WriteLine("EJEMPLOS:");
    Console.WriteLine(" dotnet run -- --help");
    Console.WriteLine(" dotnet run -- --version");
}

void MostrarBanner()
{
    Console.WriteLine("╔══════════════════════════════════════╗");
    Console.WriteLine("║   SISTEMA DE GESTIÓN DE INVENTARIO   ║");
    Console.WriteLine("╚══════════════════════════════════════╝");
    Console.WriteLine();
    Console.WriteLine($"Versión: {version}");
    Console.WriteLine($".NET: {Environment.Version}");
    Console.WriteLine($"Sistema: {Environment.OSVersion.Platform}");
    Console.WriteLine();
}

//----------------------------------------------------------------------------------------------------------------------------------------

