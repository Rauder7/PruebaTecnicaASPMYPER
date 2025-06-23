using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaMantenimientoTrabajadores.Models.Entities;

public partial class Trabajador
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El Tipo de Documento es obligatorio")]
    public string? TipoDocumento { get; set; }
    [Required(ErrorMessage = "El Número de Documento es obligatorio")]
    public string? NumeroDocumento { get; set; }
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string? Nombres { get; set; }
    [Required(ErrorMessage = "El sexo es obligatorio")]
    public string? Sexo { get; set; }
    [Required(ErrorMessage = "Debe seleccionar un departamento")]
    public int? IdDepartamento { get; set; }
    [Required(ErrorMessage = "Debe seleccionar una provincia")]
    public int? IdProvincia { get; set; }
    [Required(ErrorMessage = "Debe seleccionar un distrito")]
    public int? IdDistrito { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual Distrito? IdDistritoNavigation { get; set; }

    public virtual Provincia? IdProvinciaNavigation { get; set; }
}
