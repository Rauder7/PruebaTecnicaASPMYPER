using System;
using System.Collections.Generic;

namespace PruebaTecnicaMantenimientoTrabajadores.Models.Entities;

public partial class Provincia
{
    public int Id { get; set; }

    public int? IdDepartamento { get; set; }

    public string? NombreProvincia { get; set; }

    public virtual ICollection<Distrito> Distritos { get; set; } = new List<Distrito>();

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual ICollection<Trabajador> Trabajadores { get; set; } = new List<Trabajador>();
}
