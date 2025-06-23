using System;
using System.Collections.Generic;

namespace PruebaTecnicaMantenimientoTrabajadores.Models.Entities;

public partial class Distrito
{
    public int Id { get; set; }

    public int? IdProvincia { get; set; }

    public string? NombreDistrito { get; set; }

    public virtual Provincia? IdProvinciaNavigation { get; set; }

    public virtual ICollection<Trabajador> Trabajadores { get; set; } = new List<Trabajador>();
}
