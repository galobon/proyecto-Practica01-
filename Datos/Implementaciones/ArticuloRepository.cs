using LOCURA.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@nombre",a.Nombre),
                new SpParameter("@precio_u",a.PrecioU)
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_GUARDAR_ARTICULO", param);
        }

        public bool Update(int id, Articulo a)
        {

            List<SpParameter> param = new List<SpParameter>()
            {
                new SpParameter("@id_articulo", id),
                new SpParameter("@nombre",a.Nombre),
                new SpParameter("@precio_u",a.PrecioU)
            };

            return DataHelper.GetInstance().ExecuteSpDml("SP_ACTUALIZAR_ARTICULO", param);
        }
    }
}
