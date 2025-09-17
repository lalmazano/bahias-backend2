using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EstadoBahium
{
    public decimal IdEstado { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }
}
