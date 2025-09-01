using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Template
{
    public interface IServiceBase<TDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(decimal id);
        Task AddAsync(TDto dto);
        Task UpdateAsync(decimal id, TDto dto);
        Task DeleteAsync(decimal id);
    }
}
