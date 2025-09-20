using proyectoPratica01.Dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoPratica01.Datos.Interfaces
{
    public interface IPagoRepository
    {
        List<FormaPagoDTO> GetAll();
        FormaPagoDTO GetById(int id);
        bool Save(FormaPagoDTO fp);
        bool Delete(int id);
        bool Update(int id, FormaPagoDTO fp);
    }
}
