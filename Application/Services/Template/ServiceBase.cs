using AutoMapper;
using Infrastructure.Repositories.Template;

namespace Application.Services.Template
{
    public class ServiceBase<TDto, TEntity> : IServiceBase<TDto>
        where TDto : class
        where TEntity : class
    {
        protected readonly IRepositoryBase<TEntity> _repository;
        protected readonly IMapper _mapper;

        public ServiceBase(IRepositoryBase<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public virtual async Task<TDto?> GetByIdAsync(decimal id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TDto?>(entity);
        }

        public virtual async Task AddAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(decimal id, TDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró el elemento con ID {id}");

            _mapper.Map(dto, existing);
            _repository.Update(existing);
        }

        public virtual async Task DeleteAsync(decimal id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"No se encontró el elemento con ID {id}");

            _repository.Delete(entity);
        }
    }

}
