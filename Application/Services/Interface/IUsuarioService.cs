using Domain.DTOs;

namespace Application.Services.Interface
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto?> GetByIdAsync(decimal id);
        Task<UsuarioDto?> GetByUsernameAsync(string username);
        Task<bool> VerifyPasswordAsync(UsuarioLoginDto loginDto);
        Task AddAsync(UsuarioCreateDto UsuarioCreateDto);
        Task UpdateAsync(decimal id, UsuarioUpdateDto usuarioDto);
        Task DeleteAsync(decimal id);

    }

}
