using System;
using proyectoPratica01.Dominio;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoPratica01.Datos
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
