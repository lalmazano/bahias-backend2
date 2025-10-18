using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class EstadoReserva
{
    public decimal IdEstado { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }
}
