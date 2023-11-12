using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Models;

public partial class Asistencium
{
    public long IdCedula { get; set; }

    public string Entrada { get; set; } = null!;

    [Key]
    public int IdAsis { get; set; }

    public virtual Empleado IdCedulaNavigation { get; set; } = null!;
}

