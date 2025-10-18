using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Vehiculo
{
    public decimal IdVehiculo { get; set; }

    public string Placa { get; set; } = null!;

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public string? Color { get; set; }

    public decimal Tipo { get; set; }

    public decimal? IdUsuario { get; set; }

    public string Activo { get; set; } = null!;

    public DateTime? CreadoEn { get; set; }

    public virtual TipoVehiculo TipoNavigation { get; set; } = null!;
}
