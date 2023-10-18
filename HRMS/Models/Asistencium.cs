using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class Asistencium
{
    public long IdCedula { get; set; }

    public DateTime? Entrada { get; set; }

    public DateTime? Salida { get; set; }

    public int? Calificacion { get; set; }

    public virtual Empleado IdCedulaNavigation { get; set; } = null!;
}
