using System;
using System.Collections.Generic;

namespace HRMS.Models;

public partial class Documento
{
    public long IdCedula { get; set; }

    public byte[]? Cedula { get; set; }

    public byte[]? Contrato { get; set; }

    public byte[]? Otros { get; set; }

    public virtual Empleado IdCedulaNavigation { get; set; } = null!;
}
