using LOCURA.Datos.Interfaces;
using LOCURA.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace LOCURA.Datos.Implementaciones
{
    public class DetalleRepository : IDetalleRepository
    {
        public bool Delete(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@id", id)
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_DAR_BAJA_DETALLE_FACTURA", param);
        }

        public List<DetalleFactura> GetAll()
        {
            List<DetalleFactura> dfs = new List<DetalleFactura>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_TRAER_DETALLES_FACTURA");

            foreach (DataRow fila in dt.Rows)
            {
                DetalleFactura df = new DetalleFactura();
                df.NroFactura = new Factura();
                df.NroFactura.NroFactura = (int)fila["nro_factura"];
                df.Id = (int)fila["id_det_factura"];
                df.Articulo = new Articulo();
                df.Articulo.Id = (int)fila["id_articulo"];
                df.Cantidad = (int)fila["cantidad"];

                dfs.Add(df);
            }

            return dfs;
        }

        public DetalleFactura? GetById(int id)
        {
            DetalleFactura df = new DetalleFactura();

            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@id", id)
            };


            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_TRAER_DETALLES_FACTURA_POR_ID", param);

            if (dt.Rows.Count == 0)
                return null;

            foreach (DataRow fila in dt.Rows)
            {
                df.NroFactura = new Factura();
                df.NroFactura.NroFactura = (int)fila["nro_factura"];
                df.Id = (int)fila["id_det_factura"];
                df.Articulo = new Articulo();
                df.Articulo.Id = (int)fila["id_articulo"];
                df.Cantidad = (int)fila["cantidad"];

            }

            return df;
        }

        public bool Save(DetalleFactura df)
        {
            List<SpParameter> param = new List<SpParameter>
            {
                new SpParameter("@id_articulo", df.Articulo.Id),
                new SpParameter("@nro_factura", df.NroFactura.NroFactura),
                new SpParameter("@cantidad", df.Cantidad)
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_GUARDAR_DETALLE_FACTURAS", param);
        }

        public bool Update(int id, DetalleFactura df)
        {
            List<SpParameter> param = new List<SpParameter>
            {
                new SpParameter("@id", id),
                new SpParameter("@id_articulo", df.Articulo.Id),
                new SpParameter("@nro_factura", df.NroFactura.NroFactura),
                new SpParameter("@cantidad", df.Cantidad)
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_ACTUALIZAR_DETALLE_FACTURAS", param);
        }
    }
}
