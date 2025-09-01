using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using Application.Services.Interface;



namespace Application.Services
{
    public class UserRolService : IUserRolService
    {
        private readonly IUserRolRepository _userRolRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public UserRolService(IUserRolRepository IUserRolRepository, IUsuarioRepository IUsuarioRepository)
        {
            _userRolRepository = IUserRolRepository;
            _usuarioRepository = IUsuarioRepository;
        }

        public async Task<IEnumerable<UserRolDto>> GetAllAsync()
        {
            var userRols = await _userRolRepository.GetAllAsync();
            return userRols.Select(u => new UserRolDto
            {
                idRol = u.IdRol,
                idUsuario = u.IdUsuario,
                Assigned_at= u.AssignedAt
            });
        }

        public async Task<UserRolDto?> GetByIdAsync(decimal id)
        {
            var userRols = await _userRolRepository.GetByIdAsync(id);
            if (userRols == null) return null;

            return new UserRolDto
            {
                idRol = userRols.IdRol,
                idUsuario = userRols.IdUsuario,
                Assigned_at = userRols.AssignedAt
             };
        }

        public async Task AddAsync(UserRolDto UserRolDto)
        {
            var userrol = new UserRol
            {
                IdRol = UserRolDto.idRol,
                IdUsuario = UserRolDto.idUsuario,
             };

            await _userRolRepository.AddAsync(userrol);
        }

        public async Task UpdateAsync(decimal id, UserRolDto UserRolDto)
        {
            // Busca el ambiente en la base de datos
            var userols = await _userRolRepository.GetByIdAsync(id);
            if (userols == null)
            {
                throw new Exception("Rol no encontrado");
            }

            if (UserRolDto.idRol > 0)
            {
                userols.IdRol = UserRolDto.idRol;
            }

            if (UserRolDto.idRol > 0)
            {
                userols.IdRol = UserRolDto.idRol;
            }


            // Guarda los cambios en la base de datos
            _userRolRepository.Update(userols);
        }

        public async Task DeleteAsync(decimal id)
        {
            var userrol = await _userRolRepository.GetByIdAsync(id);
            if (userrol == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            _userRolRepository.Delete(userrol);
        }

        public async Task<List<RolesDto>> GetRolesByUserId(string username)
        {
            var usuario = await _usuarioRepository.GetByUsernameAsync(username);
            if (usuario == null)
                throw new Exception("Usuario no encontrado");
            var roles = await _userRolRepository.GetRolesByUserId(usuario.IdUsuario);

            return roles.Select(r => new RolesDto
            {
                Name = r.Name,
                Description = r.Description
            }).ToList();
        }

    }
}

