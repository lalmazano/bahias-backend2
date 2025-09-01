using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Infrastructure.Repositories.Template
{
        public class RepositoryBase<T> : IRepositoryBase<T> where T : class
        {
            protected readonly QueryContext _queryContext;
            protected readonly OperationContext _operationContext;

            public RepositoryBase(QueryContext queryContext, OperationContext operationContext)
            {
                _queryContext = queryContext;
                _operationContext = operationContext;
            }

            // Operaciones de lectura usan QueryContext
            public virtual async Task<IEnumerable<T>> GetAllAsync()
            {
                return await _queryContext.Set<T>().ToListAsync();
            }

            public async Task<T?> GetLastAsync(Expression<Func<T, decimal>> keySelector)
            {
            return await _queryContext.Set<T>()
                .OrderByDescending(keySelector)
                .FirstOrDefaultAsync();
            }


            public async Task<T?> GetByIdAsync(decimal id)
            {
                return await _queryContext.Set<T>().FindAsync(id);
            }

            public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            {
                return await _queryContext.Set<T>().Where(predicate).ToListAsync();
            }

            // Operaciones de escritura usan OperationContext
            public async Task AddAsync(T entity)
            {
                await _operationContext.Set<T>().AddAsync(entity);
                await _operationContext.SaveChangesAsync();
            }

            public void Update(T entity)
            {
                _operationContext.Set<T>().Update(entity);
                _operationContext.SaveChanges();
            }

            public void Delete(T entity)
            {
                _operationContext.Set<T>().Remove(entity);
                _operationContext.SaveChanges();
            }
        }
    }



