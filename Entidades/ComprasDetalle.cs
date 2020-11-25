using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
   public class ComprasDetalle
    {
        [Key]
        public int DetalleId { get; set; }
        public int CompraId { get; set; }
        public int ProductoId { get; set; }
        public float Costo { get; set; }
        public float Cantidad { get; set; }


    }
}
