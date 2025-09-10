using LOCURA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace LOCURA.Datos.Interfaces
{
    internal interface IPago
    {
        List<FormaPago> GetAll();
        FormaPago GetById(int id);
        bool Save(FormaPago fp);
        bool Delete(int id);
        bool Update(int id, FormaPago fp);
    }
}
