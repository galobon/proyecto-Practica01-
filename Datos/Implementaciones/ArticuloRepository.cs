using proyectoPratica01.Dominio.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyectoPratica01.Datos.Interfaces;
using proyectoPratica01.Dominio.Contexto;
using proyectoPratica01.Dominio.DTOs;

namespace proyectoPratica01.Datos.Implementaciones
{
    public class ArticuloRepository : IArticuloRepository
    {
        private ProyectoContext _context;

        public ArticuloRepository(ProyectoContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {
            var articulo = _context.Articulos.Find(id);
            if (articulo != null)
            {
                articulo.Activo = false;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<ArticuloDTO> GetAll()
        {
            return _context.Articulos
                           .Where(a => a.Activo)
                           .Select(a => new ArticuloDTO
                           {
                               IdArticulo = a.IdArticulo,
                               Nombre = a.Nombre,
                               PrecioU = a.PrecioU
                           })
                           .ToList();
        }

        public ArticuloDTO? GetById(int id)
        {
            var a = _context.Articulos.Find(id);
            if (a != null && a.Activo)
                return new ArticuloDTO
                {
                    IdArticulo = a.IdArticulo,
                    Nombre = a.Nombre,
                    PrecioU = a.PrecioU
                };
            return null;
        }

        public bool Save(ArticuloDTO a)
        {
            if(a != null)
            {
                _context.Articulos.Add(new Articulo
                {
                    Nombre = a.Nombre,
                    PrecioU = a.PrecioU,
                    Activo = true
                });
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(int id, ArticuloDTO a)
        {
            if(a != null)
            {
                var aUpdate = _context.Articulos.Find(id);
                if (aUpdate != null && aUpdate.Activo)
                {
                    aUpdate.Nombre = a.Nombre;
                    aUpdate.PrecioU = a.PrecioU;
                    _context.SaveChanges();

                    return true;
                }

            }
            return false;
        }
    }
}
