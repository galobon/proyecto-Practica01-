using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoPratica01.Dominio.DTOs
{
    public class ArticuloDTO
    {
        public int IdArticulo { get; set; }

        public string? Nombre { get; set; }

        public int? PrecioU { get; set; }
    }
}
