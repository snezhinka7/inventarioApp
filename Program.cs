using System.Reflection;

//VARIABLES---------------------------------------------------------------------------------------------------------------------
var assembly = Assembly.GetExecutingAssembly();
var version = assembly.GetName().Version;

int cantidadProductos = 0;
decimal valorTotal = 0.00m;
///string nombreSistema = "Sistema de Gestión de Inventario";
//decimal precio = 0.00m;
bool continuar = true;


//INICIALIZACIÓN-----------------------------------------------------------------------------------------------------------------
MostrarBanner();


// FUNCIONES PRINCIPALES----------------------------------------------------------------------------------------------------------
/*if (args.Length > 0)
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
}*/

while (continuar)
{
    MostrarMenu();
    string comando = LeerEntrada("Ingrese un comando: ");
    Console.WriteLine($"Comando ingresado: {comando}");
    continuar = ProcesarComando(comando);
}

/*Console.WriteLine($"Bienvenido al {nombreSistema}!");
Console.WriteLine($"Productos registrados: {cantidadProductos}");
Console.WriteLine($"Valor total del inventario: {valorTotal:N2}");
Console.WriteLine($"Sistema activo: {(continuar ? "Sí" : "No")}");

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
}/*

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

/*
Console.Write("Ingrese un comando o ingrese salir para terminar: ");
string? entrada = Console.ReadLine();

if (string.IsNullOrWhiteSpace(entrada) || entrada.ToLower() == "salir")
{
    Console.WriteLine("Saliendo del programa...");
    Environment.Exit(0);
}*/

//METODOS----------------------------------------------------------------------------------------------------------------
bool ProcesarComando(string comando)
{
    switch (comando)
    {
        case "listar":
            ListarProductos();
            return true;

        case "agregar":
            
            AgregarProducto();
            return true;

        case "buscar":
            BuscarProducto();
            return true;

        case "salir":
            Console.WriteLine("Saliendo del programa...");
            return false;

        default:
            Console.WriteLine($"Comando desconocido: {comando}");
            Console.WriteLine("Comandos disponibles: listar, agregar, buscar, salir");
            return true;
    }
}

string LeerEntrada(string prompt)
{
    Console.Write(prompt);
    string? entrada = Console.ReadLine();
    string comando = string.IsNullOrWhiteSpace(entrada) ? "salir" : entrada.Trim().ToLower();

    return comando switch
    {
        "1" => "listar",
        "2" => "agregar",
        "3" => "buscar",
        "4" => "salir",
        _ => comando
    };
}


//FUNCIONES DE INTERFAZ--------------------------------------------------------------------------------------------------
/*void MostrarAyuda()
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
}*/

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

void MostrarMenu()
{
    Console.WriteLine("MENU PRINCIPAL");
    Console.WriteLine("1. Listar productos");
    Console.WriteLine("2. Agregar producto");
    Console.WriteLine("3. Buscar producto");
    Console.WriteLine("4. Salir");
}

void ListarProductos()
{
    Console.WriteLine("LISTA DE PRODUCTOS");
    Console.WriteLine($"Total productos: {cantidadProductos}");
    Console.WriteLine($"Valor total: {valorTotal:N2}");
}

void AgregarProducto()
{
    Console.WriteLine("AGREGAR PRODUCTO");
    Console.WriteLine("Funcionalidad en desarrollo...");
}

void BuscarProducto()
{
    Console.WriteLine("BUSCAR PRODUCTO");
    Console.WriteLine("Funcionalidad en desarrollo...");
}

//----------------------------------------------------------------------------------------------------------------------------------------

