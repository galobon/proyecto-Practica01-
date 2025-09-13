using System;
using LOCURA.Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOCURA.Datos
{
    public interface IFacturaRepository
    {
        List<Factura> GetAll();
        Factura GetById(int id);
        bool Save(Factura f);
        bool Delete(int id);
        bool Update(int id, Factura f);

    }
}
