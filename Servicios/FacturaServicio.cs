using proyectoPratica01.Datos;
using proyectoPratica01.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoPratica01.Servicios
{
    public class FacturaServicio
    {
        private IFacturaRepository _reposritory;
        public FacturaServicio()
        {
            _reposritory = new FacturaRepository();
        }

        public List<Factura> GetFacturas()
        {
            return _reposritory.GetAll();
        }

        public Factura GetFactura(int id)
        {
            return _reposritory.GetById(id);
        }
        public bool SaveFactura(Factura f)
        {
            return _reposritory.Save(f);
        }
        public bool DeleteFactura(int id) 
        {
            return _reposritory.Delete(id);
        }
        public bool UpdateFactura(int id, Factura f)
        {
            return _reposritory.Update(id, f);
        }
    }
}
