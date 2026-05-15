namespace InventarioApp.Models
{
    public class Producto
    {
        private string _nombre  = string.Empty;
        private decimal _precio;
        private int _cantidad;

        public int Id { get; set; }

        public string Nombre
        {
            get => _nombre;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre del producto no puede estar vacío.", nameof(Nombre));
                _nombre = value.Trim();
            }
        }

        public decimal Precio
        {
            get => _precio;
            set => _precio = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(Precio), "El precio del producto no puede ser negativo.");
        }

        public int Cantidad
        {
            get => _cantidad;
            set => _cantidad = value >= 0 ? value : throw new ArgumentOutOfRangeException(nameof(Cantidad), "La cantidad del producto no puede ser negativa.");
        }

        public CategoriaProducto Categoria { get; set; }
        public EstadoProducto Estado { get; set; } = EstadoProducto.Activo;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public decimal ValorTotal => Precio * Cantidad;

        
    }
}