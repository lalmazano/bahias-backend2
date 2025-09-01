using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class UserRolDto
    {
        public decimal idUsuario { get; set; }
        public decimal idRol { get; set; }
        public DateTime? Assigned_at { get; set; }

    }

    public class RolesDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

