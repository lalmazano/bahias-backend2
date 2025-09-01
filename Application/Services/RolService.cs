using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using Application.Services.Template;
using Application.Services.Interface;
using AutoMapper;


namespace Application.Services
{
    public class RolService : ServiceBase<RolDto, Rol>, IRolService
    {
        private readonly IRolRepository _repo;

        public RolService(IRolRepository repo, IMapper mapper)
            : base(repo, mapper)
        {
            _repo = repo;
        }

        public override async Task AddAsync(RolDto dto)
        {
            var ultimo = await _repo.GetLastAsync(u => u.IdRol);
            var nuevoId = (ultimo?.IdRol ?? 0) + 1;

            var entity = new Rol
            {
                IdRol = nuevoId,
                Name = dto.Name,
                Description = dto.Description,
                Estado = dto.Estado
            };

            await _repo.AddAsync(entity);
        }

        public override async Task UpdateAsync(decimal id, RolDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            if (!string.IsNullOrEmpty(dto.Name))
            {
                entity.Name = dto.Name;
            }

            if (!string.IsNullOrEmpty(dto.Description))
            {
                entity.Description = dto.Description;
            }

            if (!string.IsNullOrEmpty(dto.Estado))
            {
                entity.Estado = dto.Estado;
            }
            _repo.Update(entity);
        }
    }
}