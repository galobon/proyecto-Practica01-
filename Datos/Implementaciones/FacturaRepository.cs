using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using proyectoPratica01.Datos.Interfaces;
using proyectoPratica01.Dominio.Clases;
using proyectoPratica01.Dominio.Contexto;
using proyectoPratica01.Dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoPratica01.Datos.Implementaciones
{
    public class FacturaRepository : IFacturaRepository
    {
        private ProyectoContext _context;
        public FacturaRepository(ProyectoContext context)
        {
            _context = context;
        }
        public bool Delete(int id)
        {
            var factura = _context.Facturas.Find(id);
            if (factura != null)
            {
                factura.Activo = false;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<FacturaDTO> GetAll()
        {
            var facturas = _context.Facturas
            .Where(f => f.Activo)
            .Select(f => new FacturaDTO
            {
                NroFactura = f.NroFactura,
                Fecha = f.Fecha,
                Cliente = f.Cliente,
                FormaPago = new FormaPagoDTO
                    {
                        IdFormaPago = f.IdFormaPagoNavigation.IdFormaPago,
                        Nombre = f.IdFormaPagoNavigation.Nombre
                    },
                DetallesFacturas = f.DetallesFacturas.Select(df => new DetallesDTO
                {
                    NroFactura = df.NroFactura,
                    IdDetFactura = df.IdDetFactura,
                    Cantidad = df.Cantidad,
                    Articulo = new ArticuloDTO
                    {
                        IdArticulo = df.IdArticuloNavigation.IdArticulo,
                        Nombre = df.IdArticuloNavigation.Nombre,
                        PrecioU = df.IdArticuloNavigation.PrecioU
                    }
                }).ToList()
            })
            .ToList();


            return facturas;
        }

        public FacturaDTO? GetById(int id)
        {
            var f = _context.Facturas.Include(f => f.IdFormaPagoNavigation)
                                     .Include(f => f.DetallesFacturas)
                                        .ThenInclude(df => df.IdArticuloNavigation)
                                     .FirstOrDefault(f => f.NroFactura == id && f.Activo);
            if(f != null)
            {
                return new FacturaDTO
                {
                    NroFactura = f.NroFactura,
                    Fecha = f.Fecha,
                    Cliente = f.Cliente,
                    FormaPago = new FormaPagoDTO
                    {
                        IdFormaPago = f.IdFormaPagoNavigation.IdFormaPago,
                        Nombre = f.IdFormaPagoNavigation.Nombre
                    },
                    DetallesFacturas = f.DetallesFacturas.Select(df => new DetallesDTO
                    {
                        NroFactura = df.NroFactura,
                        IdDetFactura = df.IdDetFactura,
                        Cantidad = df.Cantidad,
                        Articulo = new ArticuloDTO
                        {
                            IdArticulo = df.IdArticuloNavigation.IdArticulo,
                            Nombre = df.IdArticuloNavigation.Nombre,
                            PrecioU = df.IdArticuloNavigation.PrecioU
                        }
                    }).ToList()
                };
            }
            return null;
        }

        public bool Save(FacturaDTO f)
        {
            if (f != null)
            {
                int idDetalle = 1;
                int nextNroFactura = (_context.Facturas.Max(f => (int?)f.NroFactura) ?? 0) + 1;
                _context.Facturas.Add(new Factura
                {
                    Activo = true,
                    Fecha = f.Fecha,
                    Cliente = f.Cliente,
                    IdFormaPago = f.FormaPago.IdFormaPago,
                    DetallesFacturas = f.DetallesFacturas.Select(df => new DetallesFactura
                    {
                        NroFactura = nextNroFactura,
                        IdDetFactura = idDetalle++,
                        IdArticulo = df.Articulo.IdArticulo,
                        Cantidad = df.Cantidad
                    }).ToList()
                });
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(int id, FacturaDTO f)
        {
            var fUpdate = _context.Facturas.Include(f => f.DetallesFacturas)
                                           .FirstOrDefault(f => f.NroFactura == id && f.Activo);
            if (fUpdate != null)
            {
                int idDetalle = 1;
                fUpdate.NroFactura = id;
                fUpdate.Fecha = f.Fecha;
                fUpdate.Cliente = f.Cliente;
                fUpdate.IdFormaPago = f.FormaPago.IdFormaPago;
                fUpdate.DetallesFacturas = f.DetallesFacturas.Select(df => new DetallesFactura
                {
                    NroFactura = id,
                    IdDetFactura = idDetalle++,
                    IdArticulo = df.Articulo.IdArticulo,
                    Cantidad = df.Cantidad
                }).ToList();
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
