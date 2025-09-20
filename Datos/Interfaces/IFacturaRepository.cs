using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyectoPratica01.Dominio.DTOs;

namespace proyectoPratica01.Datos.Interfaces
{
    public interface IFacturaRepository
    {
        List<FacturaDTO> GetAll();
        FacturaDTO GetById(int id);
        bool Save(FacturaDTO f);
        bool Delete(int id);
        bool Update(int id, FacturaDTO f);

    }
}
