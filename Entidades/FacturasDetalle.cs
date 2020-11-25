using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public partial class FacturasDetalle
    {
        [Key]
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int ProductoId { get; set; }
        public float Cantidad { get; set; }
        public float Precio { get; set; }

        public FacturasDetalle(int FacturaId, int ProductoId, float cantidad, float Precio)
        {
            Id = 0;
            this.FacturaId = FacturaId;
            this.ProductoId = ProductoId;
            Cantidad = cantidad;
            this.Precio = Precio;
        }

        public FacturasDetalle(Productos producto, float cantidad)
        {
            ProductoId = producto.ProductoId;
            Cantidad = cantidad;
            Precio = producto.Precio;
        }
    }
}
