using Domain.Entities;
using Infrastructure.Database.Context;
using Infrastructure.Repositories.Template;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories.Interface;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(QueryContext queryContext, OperationContext operationContext)
            : base(queryContext, operationContext)
        {
        }

        public  override async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _queryContext.Usuarios
                .Include(u => u.UserRols)
                .ThenInclude(ur => ur.IdRolNavigation)
                .ToListAsync();
        }

        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            return await _queryContext.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}

