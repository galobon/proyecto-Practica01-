using proyectoPratica01.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoPratica01.Datos
{
    public interface IArticuloRepository
    {
        List<Articulo> GetAll();
        Articulo GetById(int id);
        bool Save(Articulo a);
        bool Delete(int id);
        bool Update(Articulo a);
    }
}
