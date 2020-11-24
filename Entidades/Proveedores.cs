using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Proveedores
    {
        [Key]
        public int ProveedorId { get; set; }

        public int UsuarioId { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public int RNC { get; set; }
        public string Telefono { get; set; }
    }
}
