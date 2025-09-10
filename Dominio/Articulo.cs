using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace LOCURA.Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int PrecioU { get; set; }

        public override string ToString()
        {
            return "ID: " + Id + " - Nombre: " + Nombre + " - Precio Unitario: " + PrecioU;
        }
    }
}
