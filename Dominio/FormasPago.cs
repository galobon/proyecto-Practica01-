using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace proyectoPratica01.Dominio;

[Table("Formas_pago")]
public partial class FormasPago
{
    [Key]
    [Column("id_forma_pago")]
    public int IdFormaPago { get; set; }

    [Column("nombre")]
    [StringLength(25)]
    [Unicode(false)]
    public string? Nombre { get; set; }
}
