using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Rol
    {
        public Rol()
        {
            UserRols = new HashSet<UserRol>();
        }

        public decimal IdRol { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<UserRol> UserRols { get; set; }
    }
}
