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
    public class PagoServicio
    {
        private IPagoRepository _repository;

        public PagoServicio(IPagoRepository repository)
        {
            _repository = repository;
        }

        public List<FormaPagoDTO> GetArticulos()
        {
            return _repository.GetAll();
        }

        public FormaPagoDTO GetArticulo(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveArticulo(FormaPagoDTO fp)
        {
            return _repository.Save(fp);
        }

        public bool DeleteArticulo(int id)
        {
            return _repository.Delete(id);
        }

        public bool UpdateArticulo(int id, FormaPagoDTO fp)
        {
            return _repository.Update(id, fp);
        }
    }
}
