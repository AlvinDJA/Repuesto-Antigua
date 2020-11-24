using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Productos
    {
        [Key]
        public int ProductoId { get; set; }
        public int MarcaId { get; set; }
        public string Nombre { get; set; }
        public float Cantidad { get; set; }
        public int NoSerie { get; set; }
        public float Precio { get; set; }
        public float Costo { get; set; }
    }
}
