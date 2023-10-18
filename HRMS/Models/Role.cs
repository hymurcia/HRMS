using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class Role
{
    public int IdMaster { get; set; }

    public string? Nombre { get; set; }

    public string? Usuario { get; set; }

    public string? Clave { get; set; }

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
