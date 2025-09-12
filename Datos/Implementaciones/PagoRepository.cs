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

        public List<FormaPago> GetAll()
        {
            List<FormaPago> fps = new List<FormaPago>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_TRAER_FORMAS_PAGO");

            foreach (DataRow fila in dt.Rows)
            {
                FormaPago fp = new FormaPago();
                fp.Id = (int)fila["id_forma_pago"];
                fp.Nombre = fila["nombre"].ToString();

                fps.Add(fp);
            }

            return fps;
        }

        public FormaPago? GetById(int id)
        {
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@id", id)
            };

            FormaPago fp = new FormaPago();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_TRAER_FORMA_PAGO_POR_ID", param);

            if(dt.Rows.Count == 0)
            {
                return null;
            }

            foreach (DataRow fila in dt.Rows)
            {
                fp.Id = (int)fila["id_forma_pago"];
                fp.Nombre = fila["nombre"].ToString();
            }

            return fp;
        }

        public bool Save(FormaPago fp)
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

        public bool Update(FormaPago fp)
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
                cmd.Parameters.AddWithValue("@id", fp.Id);
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
