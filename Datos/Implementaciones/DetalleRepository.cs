using LOCURA.Datos.Interfaces;
using LOCURA.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            bool ok = true;
            SqlConnection cnn = DataHelper.GetInstance().GetConnection();
            SqlTransaction t = null;
            SqlCommand cmd = new SqlCommand();

            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.Transaction = t;
                cmd.CommandText = "SP_GUARDAR_DETALLE_FACTURAS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_articulo", df.Articulo.Id);
                cmd.Parameters.AddWithValue("@nro_factura", df.NroFactura.NroFactura);
                cmd.Parameters.AddWithValue("@cantidad", df.Cantidad);
                cmd.ExecuteNonQuery();
                t.Commit();
            }
            catch (Exception)
            {
                if (t != null)
                    t.Rollback();
                ok = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return ok;        
        }

        public bool Update(DetalleFactura df)
        {
            bool ok = true;
            SqlConnection cnn = DataHelper.GetInstance().GetConnection();
            SqlTransaction t = null;
            SqlCommand cmd = new SqlCommand();

            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.Transaction = t;
                cmd.CommandText = "SP_ACTUALIZAR_DETALLE_FACTURAS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", df.Id);
                cmd.Parameters.AddWithValue("@id_articulo", df.Articulo.Id);
                cmd.Parameters.AddWithValue("@nro_factura", df.NroFactura.NroFactura);
                cmd.Parameters.AddWithValue("@cantidad", df.Cantidad);
                cmd.ExecuteNonQuery();
                t.Commit();
            }
            catch (Exception)
            {
                if (t != null)
                    t.Rollback();
                ok = false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }
            return ok;           
        }
    }
}
