using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Compras
    {
        [Key]
        public int CompraId { get; set; }
        public int UsuarioId { get; set; }
        public int ProveedorId { get; set; }
        public DateTime Fecha { get; set; }
        public float Itbis { get; set; }
        public float TotalCompra { get; set; }
    }
}
