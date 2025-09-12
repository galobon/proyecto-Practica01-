using LOCURA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOCURA.Datos.Interfaces
{
    public interface IDetalleRepository
    {
        List<DetalleFactura> GetAll();
        DetalleFactura GetById(int id);
        bool Save(DetalleFactura df);
        bool Delete(int id);
        bool Update(DetalleFactura df);
    }
}
