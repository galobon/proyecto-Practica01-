using System;
using LOCURA.Dominio;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;

namespace LOCURA.Datos
{
    public class FacturaRepository : IFacturaRepository
    {
        public bool Delete(int id)
        {
            List<SpParameter> param = new List<SpParameter>() { new SpParameter("@id", id) };

            return DataHelper.GetInstance().ExecuteSpDml("SP_DAR_BAJA_FACTURA", param);
        }

        public List<Factura> GetAll()
        {
            List<Factura> facturas = new List<Factura>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_TRAER_FACTURAS");

            foreach (DataRow fila in dt.Rows)
            {
                Factura f = new Factura();
                f.NroFactura = (int)fila["nro_factura"];
                f.Fecha = (DateTime)fila["fecha"];
                f.FormaPago = new FormaPago();
                f.FormaPago.Nombre = fila["nombre"].ToString();
                f.Cliente = fila["cliente"].ToString();

                facturas.Add(f);
            }

            return facturas;
        }

        public Factura? GetById(int id)
        {
            Factura f = new Factura();

            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter()
                {
                    Name = "@id",
                    Valor = id
                }
            };  

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_TRAER_FACTURAS_POR_ID", param);


            if (dt.Rows.Count == 0)
                return null;
            else
                foreach (DataRow fila in dt.Rows)
                {
                    f.NroFactura = (int)fila["nro_factura"];
                    f.Fecha = (DateTime)fila["fecha"];
                    f.FormaPago = new FormaPago();
                    f.FormaPago.Nombre = fila["nombre"].ToString();
                    f.Cliente = fila["cliente"].ToString();
                }

             return f;
        }

        public bool Save(Factura f)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@fecha", f.Fecha),    
                new SpParameter("@id_forma_pago",f.FormaPago.Id),
                new SpParameter("@cliente",f.Cliente)               
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_GUARDAR_FACTURA", param);
        }

        public bool Update(int id, Factura f)
        {

            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@nro_factura", id),
                new SpParameter("@fecha", f.Fecha),
                new SpParameter("@id_forma_pago",f.FormaPago.Id),
                new SpParameter("@cliente",f.Cliente)
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_ACTUALIZAR_FACTURA", param);

        }
    }
}
