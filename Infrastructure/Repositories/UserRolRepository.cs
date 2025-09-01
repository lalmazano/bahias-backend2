using Domain.Entities;
using Infrastructure.Database.Context;
using Infrastructure.Repositories.Template;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.Interface;
using Domain.DTOs;

namespace Infrastructure.Repositories
{
    public class UserRolRepository : RepositoryBase<UserRol>, IUserRolRepository
    {
        public UserRolRepository(QueryContext queryContext, OperationContext operationContext)
      : base(queryContext, operationContext)
        {
        }

        public async Task<List<RolesDto>> GetRolesByUserId(decimal userId)
        {
            return await _queryContext.UserRols
                .Where(ur => ur.IdUsuario == userId)
                .Include(ur => ur.IdRolNavigation) // Asegura que la relación se cargue
                .Select(ur => new RolesDto // Retorna RolesDto en lugar de UserRol
                {
                    Name = ur.IdRolNavigation.Name,
                    Description = ur.IdRolNavigation.Description
                })
                .ToListAsync();
        }

    }
}