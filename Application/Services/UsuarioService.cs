using Infrastructure.Repositories.Interface;
using Application.Services.Interface;
using Shared.Security.Encrypt;
using Domain.DTOs;
using Domain.Entities;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDataEncriptada _dataEncriptada;

        public UsuarioService(IUsuarioRepository usuarioRepository, IDataEncriptada dataEncriptada)
        {
            _usuarioRepository = usuarioRepository;
            _dataEncriptada = dataEncriptada;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return usuarios.Select(u => new UsuarioDto
            {
                Id = u.IdUsuario,
                Username = u.Username,
                Nombre = u.Name,
                Apellido= u.Lastname,
                Email = u.Email ?? string.Empty,
                Estado = u.Estado ?? "A",
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                Roles = u.UserRols?.Select(ur => ur.IdRolNavigation.Name).ToList() ?? new List<string>()

            });
        }

        public async Task<UsuarioDto?> GetByIdAsync(decimal id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) return null;

            return new UsuarioDto
            {
                Id = usuario.IdUsuario,
                Username = usuario.Username,
                Email = usuario.Email ?? string.Empty,
                Estado = usuario.Estado ?? "A",
                Nombre = usuario.Name,
                Apellido = usuario.Lastname
            };
        }

        public async Task<UsuarioDto?> GetByUsernameAsync(string username)
        {
            var usuario = await _usuarioRepository.GetByUsernameAsync(username);
            if (usuario == null) return null;

            return new UsuarioDto
            {
                Id = usuario.IdUsuario,
                Username = usuario.Username,
                Email = usuario.Email ?? string.Empty,
                Estado = usuario.Estado ?? "A",
                Nombre = usuario.Name ?? string.Empty,
                Apellido = usuario.Lastname ?? string.Empty

            };
        }

        public async Task AddAsync(UsuarioCreateDto UsuarioCreateDto)
        {
            var hashedPassword = _dataEncriptada.EncriptarData(UsuarioCreateDto.Password);
            var ultimoUsuario = await _usuarioRepository.GetLastAsync(u => u.IdUsuario);
            var nuevoId = (ultimoUsuario?.IdUsuario ?? 0) + 1;

            var usuario = new Usuario
            {
                IdUsuario = nuevoId,
                Username = UsuarioCreateDto.Username,
                Email = UsuarioCreateDto.Email,
                Estado = UsuarioCreateDto.Estado,
                Password = hashedPassword,
                Name = UsuarioCreateDto.Nombre,
                Lastname = UsuarioCreateDto.Apellido

            };

            await _usuarioRepository.AddAsync(usuario);
        }

        public async Task<bool> VerifyPasswordAsync(UsuarioLoginDto loginDto)
        {
            var usuario = await _usuarioRepository.GetByUsernameAsync(loginDto.Username);
            if (usuario == null) return false;

            return _dataEncriptada.VerifyPassword(loginDto.Password, usuario.Password);
        }

        public async Task UpdateAsync(decimal id, UsuarioUpdateDto usuarioDto)
        {
            // Busca el usuario en la base de datos
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            // Actualiza los datos del usuario
            if (!string.IsNullOrEmpty(usuarioDto.Username))
            {
                usuario.Username = usuarioDto.Username;
            }

            if (!string.IsNullOrEmpty(usuarioDto.Email))
            {
                usuario.Email = usuarioDto.Email;
            }

            if (!string.IsNullOrEmpty(usuarioDto.Email))
            {
                usuario.Estado = usuarioDto.Estado;
            }

            if (!string.IsNullOrEmpty(usuarioDto.Password))
            {
                usuario.Password = _dataEncriptada.EncriptarData(usuarioDto.Password);
            }

            if (!string.IsNullOrEmpty(usuarioDto.Nombre))
            {
                usuario.Name= usuarioDto.Nombre;
            }

            if (!string.IsNullOrEmpty(usuarioDto.Apellido))
            {
                usuario.Lastname = usuarioDto.Apellido;
            }

            _usuarioRepository.Update(usuario);
        }


        public async Task DeleteAsync(decimal id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null) throw new Exception("Usuario no encontrado");

            _usuarioRepository.Delete(usuario);
        }
    }
}
