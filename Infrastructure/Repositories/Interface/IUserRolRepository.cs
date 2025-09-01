using Domain.Entities;
using Infrastructure.Repositories.Template;
using Domain.DTOs;

namespace Infrastructure.Repositories.Interface
{
    public interface IUserRolRepository : IRepositoryBase<UserRol>
    {
        Task<List<RolesDto>> GetRolesByUserId(decimal userId);
    }
}