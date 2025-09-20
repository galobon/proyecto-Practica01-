using proyectoPratica01.Datos;
using proyectoPratica01.Datos.Interfaces;
using proyectoPratica01.Dominio;
using proyectoPratica01.Dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoPratica01.Servicios
{
    public class FacturaServicio
    {
        private IFacturaRepository _repository;
        public FacturaServicio(IFacturaRepository repository)
        {
            _repository = repository;
        }

        public List<FacturaDTO> GetFacturas()
        {
            return _repository.GetAll();
        }

        public FacturaDTO GetFactura(int id)
        {
            return _repository.GetById(id);
        }
        public bool SaveFactura(FacturaDTO f)
        {
            return _repository.Save(f);
        }
        public bool DeleteFactura(int id) 
        {
            return _repository.Delete(id);
        }
        public bool UpdateFactura(int id, FacturaDTO f)
        {
            return _repository.Update(id, f);
        }
    }
}
