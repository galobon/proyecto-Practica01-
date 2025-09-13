using LOCURA.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            string detallesStr = Detalles != null ? string.Join("\n", Detalles) : "";

            return "Numero de la factura: " + NroFactura + "\n" +
                   "Cliente: " + Cliente + "\n" +
                   "Forma de pago: " + FormaPago + "\n" +
                   "Fecha: " + Fecha + "\n" +
                   detallesStr + "\n" +
                   "-----------------------------------------";
        }
    }
}
