using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Context
{
    public class QueryContext : ModelContext
    {
        public QueryContext(DbContextOptions<QueryContext> options)
        : base(options)
        {
        }
    }
}

