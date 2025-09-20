using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoPratica01.Dominio.DTOs
{
    public class DetallesDTO
    {
        public int IdDetFactura { get; set; }

        public int NroFactura { get; set; }

        public int? Cantidad { get; set; }

        public ArticuloDTO Articulo { get; set; }
    }
}
