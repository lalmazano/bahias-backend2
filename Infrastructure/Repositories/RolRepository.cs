using Domain.Entities;
using Infrastructure.Database.Context;
using Infrastructure.Repositories.Template;
using Infrastructure.Repositories.Interface;

namespace Infrastructure.Repositories
{
    public class RolRepository : RepositoryBase<Rol>, IRolRepository
    {
        public RolRepository(QueryContext queryContext, OperationContext operationContext)
      : base(queryContext, operationContext)
        {
        }

    }
    
}
