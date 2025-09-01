using Domain.DTOs;

namespace Application.Services.Interface
{
    public interface IUserRolService
    {
        Task<IEnumerable<UserRolDto>> GetAllAsync();
        Task<UserRolDto?> GetByIdAsync(decimal id);
        Task AddAsync(UserRolDto UserRolDto);
        Task UpdateAsync(decimal id, UserRolDto RolDto);
        Task DeleteAsync(decimal id);
        Task<List<RolesDto>> GetRolesByUserId(string username);
    }

}


