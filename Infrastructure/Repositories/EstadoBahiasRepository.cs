using Domain.Entities;
using Infrastructure.Database.Context;
using Infrastructure.Repositories.Interface;
using Infrastructure.Repositories.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EstadoBahiasRepository : RepositoryBase<EstadoBahium>, IEstadoBahiasRepository
    {
        public EstadoBahiasRepository(QueryContext queryContext, OperationContext operationContext)
      : base(queryContext, operationContext)
        {
        }

    }
}


