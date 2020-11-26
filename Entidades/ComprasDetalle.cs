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

        public ComprasDetalle(int CompraId, int ProductoId, float Costo, float Cantidad)
        {
            this.DetalleId = 0;
            this.CompraId = CompraId;
            this.ProductoId = ProductoId;
            this.Costo = Cantidad;
            this.Cantidad = Cantidad;
        }

        public ComprasDetalle(Productos producto, float cantidad)
        {
            ProductoId = producto.ProductoId;
            Cantidad = cantidad;
            Costo = producto.Costo;
        }

    }
}
