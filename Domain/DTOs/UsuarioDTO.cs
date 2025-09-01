using System.ComponentModel.DataAnnotations;


namespace Domain.DTOs
{
    public class UsuarioBase
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Estado { get; set; } = "A";
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
    }
    

    public class UsuarioDto : UsuarioBase
        {
        public decimal Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<string>? Roles { get; set; } = new List<string>();

    }

    public class UsuarioCreateDto : UsuarioBase
        {
        public decimal Id { get; set; }
        public string Password { get; set; } = string.Empty;
    }

    public class UsuarioLoginDto
        {
        private string _username { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string Username
        {
            get => _username;
            set => _username = value.ToUpper();
        }
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set; } = string.Empty;
    }

    public class UsuarioUpdateDto : UsuarioBase
    {
        public string? Password { get; set; } 
    }
}

