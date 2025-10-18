using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class TipoVehiculo
{
    public decimal IdTipo { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? Icon { get; set; }

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
