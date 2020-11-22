using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Clave { get; set; }
        public string Nombres { get; set; }

    }
}
