using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoPratica01.Dominio.DTOs
{
    public class FacturaDTO
    {
        public int NroFactura { get; set; }

        public DateOnly? Fecha { get; set; }

        public FormaPagoDTO FormaPago { get; set; }

        public string Cliente { get; set; }

        public List<DetallesDTO> DetallesFacturas { get; set; }
    }
}
