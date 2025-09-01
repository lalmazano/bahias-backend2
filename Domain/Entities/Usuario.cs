using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Usuario
    {
        public Usuario()
        {
           UserRols = new HashSet<UserRol>();
        }

        public decimal IdUsuario { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? Estado { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public virtual ICollection<UserRol> UserRols { get; set; }
    }
}
