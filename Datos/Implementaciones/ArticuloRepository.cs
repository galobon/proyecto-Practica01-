using LOCURA.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace LOCURA.Datos
{
    public class ArticuloRepository : IArticuloRepository
    {
        public bool Delete(int id)
        {
            List<SpParameter> param = new List<SpParameter>() { new SpParameter("@id", id) };

            return DataHelper.GetInstance().ExecuteSpDml("SP_DAR_BAJA_ARTICULO", param);
        }

        public List<Articulo> GetAll()
        {
            List<Articulo> articulos = new List<Articulo>();

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_TRAER_ARTICULOS");

            foreach (DataRow fila in dt.Rows)
            {
                Articulo a = new Articulo();
                a.Id = (int)fila["id_articulo"];
                a.Nombre = fila["nombre"].ToString();
                a.PrecioU = (int)fila["precio_u"];

                articulos.Add(a);
            }

            return articulos;
        }

        public Articulo? GetById(int id)
        {
            Articulo a = new Articulo();

            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter()
                {
                    Name = "@id",
                    Valor = id
                }
            };

            var dt = DataHelper.GetInstance().ExecuteSPQuery("SP_TRAER_ARTICULOS_POR_ID", param);


            if (dt.Rows.Count == 0)
                return null;
            else
                foreach (DataRow fila in dt.Rows)
                {
                    a.Id = (int)fila["id_articulo"];
                    a.Nombre = fila["nombre"].ToString();
                    a.PrecioU = (int)fila["precio_u"];
                }

            return a;
        }

        public bool Save(Articulo a)
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
                cmd.CommandText = "SP_GUARDAR_ARTICULO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", a.Nombre);
                cmd.Parameters.AddWithValue("@precio_u", a.PrecioU);
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

        public bool Update(Articulo a)
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
                cmd.CommandText = "SP_ACTUALIZAR_ARTICULO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_articulo", a.Id);
                cmd.Parameters.AddWithValue("@nombre", a.Nombre);
                cmd.Parameters.AddWithValue("@precio_u", a.PrecioU);
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
