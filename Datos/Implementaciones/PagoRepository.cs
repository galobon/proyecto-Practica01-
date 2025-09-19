using proyectoPratica01.Datos.Interfaces;
using proyectoPratica01.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoPratica01.Datos.Implementaciones
{
    public class PagoRepository : IPago
    {
        public bool Delete(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@id", id)
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_DAR_BAJA_FORMA_PAGO", param);
        }

        public List<FormasPago> GetAll()
        {
            List<FormasPago> fps = new List<FormasPago>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_TRAER_FORMAS_PAGO");

            foreach (DataRow fila in dt.Rows)
            {
                FormasPago fp = new FormasPago();
                fp.IdFormaPago = (int)fila["id_forma_pago"];
                fp.Nombre = fila["nombre"].ToString();

                fps.Add(fp);
            }

            return fps;
        }

        public FormasPago? GetById(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@id", id)
            };

            FormasPago fp = new FormasPago();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_TRAER_FORMA_PAGO_POR_ID", param);

            if(dt.Rows.Count == 0)
            {
                return null;
            }

            foreach (DataRow fila in dt.Rows)
            {
                fp.IdFormaPago= (int)fila["id_forma_pago"];
                fp.Nombre = fila["nombre"].ToString();
            }

            return fp;
        }

        public bool Save(FormasPago fp)
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
                cmd.CommandText = "SP_GUARDAR_FORMA_PAGO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", fp.Nombre);
                cmd.ExecuteNonQuery();
                t.Commit();
            }
            catch (Exception)
            {
                if(t != null)
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

        public bool Update(FormasPago fp)
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
                cmd.CommandText = "SP_ACTUALIZAR_FORMAS_PAGO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", fp.IdFormaPago);
                cmd.Parameters.AddWithValue("@nombre", fp.Nombre);
                cmd.ExecuteNonQuery();
                t.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                if (t != null) t.Rollback();
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
