using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyectoPratica01.Dominio;

public partial class Factura
{
    [Key]
    [Column("nro_factura")]
    public int NroFactura { get; set; }

    [Column("fecha")]
    public DateTime? Fecha { get; set; }

    [Column("id_forma_pago")]
    public int? IdFormaPago { get; set; }

    [Column("cliente")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Cliente { get; set; }

    [InverseProperty("NroFacturaNavigation")]
    public virtual ICollection<DetallesFactura> DetallesFacturas { get; set; } = new List<DetallesFactura>();
}
