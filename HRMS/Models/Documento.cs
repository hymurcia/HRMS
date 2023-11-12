using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Models;

public partial class Documento
{
    [Key]
    public long IdCedula { get; set; }

    public byte[]? Cedula { get; set; }

    public byte[]? Contrato { get; set; }

    public byte[]? Otros { get; set; }

    public virtual Empleado IdCedulaNavigation { get; set; } = null!;
}
