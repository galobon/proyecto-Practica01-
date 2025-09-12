using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOCURA.Dominio
{
    public class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public FormaPago FormaPago { get; set; }
        public string Cliente { get; set; }
        public List<DetalleFactura> Detalles {  get; set; }

        public override string ToString()
        {
            return "Numero de la factura: " + NroFactura + "\n" +
                   "Cliente: " + Cliente + "\n" +
                   "Forma de pago: " + FormaPago + "\n" +
                   "Fecha: " + Fecha + "\n" +
                   "-----------------------------------------";
        }
    }
}
