using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entidades
{
    public class Marcas
    {
        [Key]
        public int MarcaId { get; set; }
        public string Nombres { get; set; }

    }
}
