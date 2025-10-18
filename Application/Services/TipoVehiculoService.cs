using Application.Services.Interface;
using Application.Services.Template;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TipoVehiculoService : ServiceBase<TipoVehiculo, TipoVehiculo>, ITipoVehiculoService
    {
        private readonly ITipoVehiculoRepository _repo;

        public TipoVehiculoService(ITipoVehiculoRepository repo, IMapper mapper)
            : base(repo, mapper)
        {
            _repo = repo;
        }

        public override async Task AddAsync(TipoVehiculo dto)
        {
            var ultimo = await _repo.GetLastAsync(u => u.IdTipo);
            var nuevoId = (ultimo?.IdTipo ?? 0) + 1;

            var entity = new TipoVehiculo
            {
                IdTipo = nuevoId,
                Descripcion = dto.Descripcion,
                Nombre = dto.Nombre
            };

            await _repo.AddAsync(entity);
        }

        public override async Task UpdateAsync(decimal id, TipoVehiculo dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Tipo de Vehículo no encontrado");
            }

            if (!string.IsNullOrEmpty(dto.Descripcion))
            {
                entity.Descripcion = dto.Descripcion;
            }

            if (!string.IsNullOrEmpty(dto.Nombre))
            {
                entity.Nombre = dto.Nombre;
            }

            _repo.Update(entity);
        }
    }
}