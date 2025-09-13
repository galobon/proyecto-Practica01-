using System;
using LOCURA.Dominio;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Data.SqlClient;

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
                f.FormaPago.Id = (int)fila["id_forma_pago"];
                f.FormaPago.Nombre = fila["nombre"].ToString();
                f.Cliente = fila["cliente"].ToString();
                f.Detalles = new List<DetalleFactura>();
                var param = new List<SpParameter>() { new SpParameter("@id", f.NroFactura) };
                var dtDetalles = DataHelper.GetInstance().ExecuteSPQuery("SP_TRAER_DETALLES_FACTURA", param);

                foreach (DataRow filaDetalle in dtDetalles.Rows)
                {
                    DetalleFactura df = new DetalleFactura();
                    df.NroFactura = f.NroFactura;
                    df.Id = (int)filaDetalle["id_det_factura"];
                    df.Cantidad = (int)filaDetalle["cantidad"];
                    df.Articulo = new Articulo();
                    df.Articulo.Id = (int)filaDetalle["id_articulo"];
                    df.Articulo.Nombre = filaDetalle["articulo"].ToString();
                    df.Articulo.PrecioU = (int)filaDetalle["precio_u"];

                    f.Detalles.Add(df);
                }

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
                    f.FormaPago.Id = (int)fila["id_forma_pago"];
                    f.FormaPago.Nombre = fila["nombre"].ToString();
                    f.Cliente = fila["cliente"].ToString();
                    f.Detalles = new List<DetalleFactura>();

                    var paramDet = new List<SpParameter>() { new SpParameter("@id", f.NroFactura) };
                    var dtDetalles = DataHelper.GetInstance().ExecuteSPQuery("SP_TRAER_DETALLES_FACTURA", param);

                    foreach (DataRow filaDetalle in dtDetalles.Rows)
                    {
                        DetalleFactura df = new DetalleFactura();
                        df.NroFactura = f.NroFactura;
                        df.Id = (int)filaDetalle["id_det_factura"];
                        df.Cantidad = (int)filaDetalle["cantidad"];
                        df.Articulo = new Articulo();
                        df.Articulo.Id = (int)filaDetalle["id_articulo"];
                        df.Articulo.Nombre = filaDetalle["articulo"].ToString();
                        df.Articulo.PrecioU = (int)filaDetalle["precio_u"];

                        f.Detalles.Add(df);
                    }
                }

             return f;
        }

        public bool Save(Factura f)
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
                cmd.CommandText = "SP_GUARDAR_FACTURA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha", f.Fecha);
                cmd.Parameters.AddWithValue("@id_forma_pago", f.FormaPago.Id);
                cmd.Parameters.AddWithValue("@cliente", f.Cliente);

                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@nro_factura";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pOut);
                cmd.ExecuteNonQuery();

                SqlCommand cmdDetalle;
                int nroFactura = (int)pOut.Value;
                int idDetalle = 1;

                foreach (DetalleFactura item in f.Detalles)
                {
                    cmdDetalle = new SqlCommand("SP_GUARDAR_DETALLE_FACTURAS",cnn,t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@id_det_factura", idDetalle);
                    cmdDetalle.Parameters.AddWithValue("@id_articulo", item.Articulo.Id);
                    cmdDetalle.Parameters.AddWithValue("@nro_factura", nroFactura);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                    cmdDetalle.ExecuteNonQuery();
                    idDetalle++;
                }
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

        public bool Update(int id, Factura f)
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
                cmd.CommandText = "SP_ACTUALIZAR_FACTURA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nro_factura", id);
                cmd.Parameters.AddWithValue("@fecha", f.Fecha);
                cmd.Parameters.AddWithValue("@id_forma_pago", f.FormaPago.Id);
                cmd.Parameters.AddWithValue("@cliente", f.Cliente);
                cmd.ExecuteNonQuery();

                SqlCommand cmdDetalle;
                int idDetalle = 1;
                foreach (DetalleFactura item in f.Detalles)
                {
                    cmdDetalle = new SqlCommand("SP_ACTUALIZAR_DETALLE_FACTURAS", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@id_det_factura", idDetalle);
                    cmdDetalle.Parameters.AddWithValue("@id_articulo", item.Articulo.Id);
                    cmdDetalle.Parameters.AddWithValue("@nro_factura", id);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                    cmdDetalle.ExecuteNonQuery();
                    idDetalle++;
                }
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
                if(cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            return ok;
        }
    }
}
