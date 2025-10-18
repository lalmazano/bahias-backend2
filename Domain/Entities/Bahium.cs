using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Bahium
{
    public decimal IdBahia { get; set; }

    public decimal? IdUbicacion { get; set; }

    public decimal IdEstado { get; set; }

    public decimal? IdReserva { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual Reserva? IdReservaNavigation { get; set; }

    public virtual Ubicacion? IdUbicacionNavigation { get; set; }
}
