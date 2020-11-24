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
        //public int ClienteId { get; set; }
        public DateTime Fecha { get; set; }
        public double Itbis { get; set; }
        public double Total { get; set; }
    }
}
