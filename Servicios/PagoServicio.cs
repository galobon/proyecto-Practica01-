using LOCURA.Datos;
using LOCURA.Datos.Implementaciones;
using LOCURA.Datos.Interfaces;
using LOCURA.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOCURA.Servicios
{
    public class PagoServicio
    {
        private IPago _repository;

        public PagoServicio()
        {
            _repository = new PagoRepository();
        }

        public List<FormaPago> GetArticulos()
        {
            return _repository.GetAll();
        }

        public FormaPago GetArticulo(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveArticulo(FormaPago fp)
        {
            return _repository.Save(fp);
        }

        public bool DeleteArticulo(int id)
        {
            return _repository.Delete(id);
        }

        public bool UpdateArticulo(FormaPago fp)
        {
            return _repository.Update(fp);
        }
    }
}
