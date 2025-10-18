using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Parametro
{
    public decimal IdParametro { get; set; }

    public string Clave { get; set; } = null!;

    public string Valor { get; set; } = null!;

    public string? Descripcion { get; set; }
}
