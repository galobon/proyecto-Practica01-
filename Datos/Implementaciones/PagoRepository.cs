using proyectoPratica01.Datos.Implementaciones;
using proyectoPratica01.Dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using proyectoPratica01.Dominio.Contexto;
using proyectoPratica01.Datos.Interfaces;
using proyectoPratica01.Dominio.Clases;

namespace proyectoPratica01.Datos.Implementaciones
{
    public class PagoRepository : IPagoRepository
    {
        private ProyectoContext _context;
        public PagoRepository(ProyectoContext context)
        {
            _context = context;
        }

        public bool Delete(int id)
        {

            var pago = _context.FormasPagos.Find(id);
            if (pago != null)
            {
                pago.Activo = false;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<FormaPagoDTO> GetAll()
        {
            return _context.FormasPagos
                           .Where(p => p.Activo)
                           .Select(p => new FormaPagoDTO
                           {
                           IdFormaPago = p.IdFormaPago,
                           Nombre = p.Nombre
                           })
                           .ToList();
        }

        public FormaPagoDTO? GetById(int id)
        {
            var pago = _context.FormasPagos.Find(id);
            if (pago != null && pago.Activo)
                return new FormaPagoDTO
                {
                    IdFormaPago = pago.IdFormaPago,
                    Nombre = pago.Nombre
                };
            return null;
        }

        public bool Save(FormaPagoDTO fp)
        {
            if (fp != null)
            {
                _context.FormasPagos.Add(new FormasPago
                {
                    Nombre = fp.Nombre,
                    Activo = true
                });
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(int id, FormaPagoDTO fp)
        {
            if (fp != null)
            {
                var pago = _context.FormasPagos.Find(id);
                if (pago != null && pago.Activo)
                {
                    pago.Nombre = fp.Nombre;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
