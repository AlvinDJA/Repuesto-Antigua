using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public partial class Facturas
    {
        [Key]
        public int FacturaId { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public float Itbis { get; set; }
        public float Total { get; set; }

        [ForeignKey("FacturaId")]
        public List<FacturasDetalle> Detalle { get; set; } = new List<FacturasDetalle>();
    }
}
