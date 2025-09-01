using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class UserRol
    {
        public decimal IdUsuario { get; set; }
        public decimal IdRol { get; set; }
        public DateTime? AssignedAt { get; set; }

        public virtual Rol IdRolNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
