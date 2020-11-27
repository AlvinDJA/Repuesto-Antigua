using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public String Cedula { get; set; }
        public String Direccion { get; set; }
        public String Nombres { get; set; }
        public String Apellidos { get; set; }
        public String Telefono { get; set; }
        public String Correo { get; set; }
        public String Celular { get; set; }
    }
}
