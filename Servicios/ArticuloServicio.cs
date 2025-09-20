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
    public class ArticuloServicio
    {
        private IArticuloRepository _repository;

        public ArticuloServicio(IArticuloRepository repository)
        {
            _repository = repository;
        }

        public List<ArticuloDTO> GetArticulos()
        {
            return _repository.GetAll();
        }

        public ArticuloDTO GetArticulo(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveArticulo(ArticuloDTO a)
        {
            return _repository.Save(a);
        }

        public bool DeleteArticulo(int id)
        {
            return _repository.Delete(id);
        }

        public bool UpdateArticulo(int id, ArticuloDTO a)
        {
            return _repository.Update(id, a);
        }

    }
}
