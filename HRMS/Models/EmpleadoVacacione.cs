using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class EmpleadoVacacione
{
    public long? IdCedula { get; set; }

    public long? Id { get; set; }

    public virtual Empleado? IdCedulaNavigation { get; set; }

    public virtual VacacionesAusencia? IdNavigation { get; set; }
}
