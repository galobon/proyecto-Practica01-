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
    public class DetalleServicio
    {
        IDetalleRepository _repository;

        public DetalleServicio()
        {
            _repository = new DetalleRepository();
        }

        public List<DetalleFactura> GetDetalles()
        {
            return _repository.GetAll();
        }

        public DetalleFactura GetDetalle(int id)
        {
            return _repository.GetById(id);
        }

        public bool SaveDetalle(DetalleFactura df)
        {
            return _repository.Save(df);
        }

        public bool DeleteDetalle(int id)
        {
            return _repository.Delete(id);
        }

        public bool UpdateDetalle(int id, DetalleFactura df)
        {
            return _repository.Update(id, df);
        }
    }
}
