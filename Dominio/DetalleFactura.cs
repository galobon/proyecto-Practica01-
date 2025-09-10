using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOCURA.Dominio
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public Factura NroFactura { get; set; }
        public Articulo Articulo { get; set; }
        public int Cantidad { get; set; }

        public override string ToString()
        {
            return "ID: " +Id + " - Factura: " + NroFactura.NroFactura + " - Artículo: " + Articulo.Id + " - Cantidad: " + Cantidad;
        }
    }
}
