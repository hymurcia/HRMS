using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class VacacionesAusencia
{
    public long Id { get; set; }

    public DateTime? Inicio { get; set; }

    public DateTime? Finalizacion { get; set; }

    public string? Motivo { get; set; }

    public string? Estado { get; set; }
}
