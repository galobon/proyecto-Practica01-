using proyectoPratica01.Datos;
using proyectoPratica01.Datos.Implementaciones;
using proyectoPratica01.Datos.Interfaces;
using proyectoPratica01.Dominio;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoPratica01.Servicios
{
    public class PagoServicio
    {
        private IPago _repository;

        public PagoServicio()
        {
            _repository = new PagoRepository();
        }

        public List<FormasPago> GetArticulos()
        {
            return _repository.GetAll();
        }

        public FormasPago GetArticulo(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveArticulo(FormasPago fp)
        {
            return _repository.Save(fp);
        }

        public bool DeleteArticulo(int id)
        {
            return _repository.Delete(id);
        }

        public bool UpdateArticulo(FormasPago fp)
        {
            return _repository.Update(fp);
        }
    }
}
