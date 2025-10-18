using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Ubicacion
{
    public decimal IdUbicacion { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Detalle { get; set; }

    public virtual ICollection<Bahium> Bahia { get; set; } = new List<Bahium>();
}
