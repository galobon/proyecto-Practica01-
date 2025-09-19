using proyectoPratica01.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace proyectoPratica01.Datos.Interfaces
{
    internal interface IPago
    {
        List<FormasPago> GetAll();
        FormasPago GetById(int id);
        bool Save(FormasPago fp);
        bool Delete(int id);
        bool Update(FormasPago fp);
    }
}
