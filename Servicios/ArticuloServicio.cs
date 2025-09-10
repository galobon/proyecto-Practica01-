using LOCURA.Datos;
using LOCURA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOCURA.Servicios
{
    public class ArticuloServicio
    {
        private IArticuloRepository _repository;

        public ArticuloServicio()
        {
            _repository = new ArticuloRepository();
        }

        public List<Articulo> GetArticulos()
        {
            return _repository.GetAll();
        }

        public Articulo GetArticulo(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveArticulo(Articulo a)
        {
            return _repository.Save(a);
        }

        public bool DeleteArticulo(int id)
        {
            return _repository.Delete(id);
        }

        public bool UpdateArticulo(int id, Articulo a)
        {
            return _repository.Update(id, a);
        }

    }
}
