using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class Empleado
{
    public long Cedula { get; set; }

    public string? Nombre { get; set; }

    public long? Telefono { get; set; }

    public string? Direccion { get; set; }

    public string? Correo { get; set; }

    public int? IdDepartamento { get; set; }

    public DateTime? FechaContratacion { get; set; }

    public int? IdRol { get; set; }

    public virtual Departamento? IdDepartamentoNavigation { get; set; }

    public virtual Role? IdRolNavigation { get; set; }
}
