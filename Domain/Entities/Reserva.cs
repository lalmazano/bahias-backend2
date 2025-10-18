using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Reserva
{
    public decimal IdReserva { get; set; }

    public decimal IdUsuario { get; set; }

    public DateTime InicioTs { get; set; }

    public DateTime FinTs { get; set; }

    public decimal? IdVehiculo { get; set; }

    public decimal Estado { get; set; }

    public string? Observacion { get; set; }

    public DateTime? CreadoEn { get; set; }

    public virtual ICollection<Bahium> Bahia { get; set; } = new List<Bahium>();
}
