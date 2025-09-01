using Domain.Entities;
using Infrastructure.Repositories.Template;

namespace Infrastructure.Repositories.Interface
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<Usuario?> GetByUsernameAsync(string username);
    }
}
