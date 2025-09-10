using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOCURA.Datos
{
    public class SpParameter
    {
        public string Name { get; set; }
        public object Valor { get; set; }

        public SpParameter() { }

        public SpParameter(string name, object valor)
        {
            this.Name = name;
            this.Valor = valor;
        }
    }
}
