using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyectoPratica01.Dominio;

[PrimaryKey("NroFactura", "IdDetFactura")]
[Table("Detalles_factura")]
public partial class DetallesFactura
{
    [Key]
    [Column("id_det_factura")]
    public int IdDetFactura { get; set; }

    [Key]
    [Column("nro_factura")]
    public int NroFactura { get; set; }

    [Column("cantidad")]
    public int? Cantidad { get; set; }

    public Articulo Articulo { get; set; }
}
