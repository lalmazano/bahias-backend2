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
    public class UbicacionService: ServiceBase<Ubicacion, Ubicacion>, IUbicacionService
    {
        private readonly IUbicacionRepository _repo;

        public UbicacionService(IUbicacionRepository repo, IMapper mapper)
            : base(repo, mapper)
        {
            _repo = repo;
        }

        public override async Task AddAsync(Ubicacion dto)
        {
            var ultimo = await _repo.GetLastAsync(u => u.IdUbicacion);
            var nuevoId = (ultimo?.IdUbicacion ?? 0) + 1;

            var entity = new Ubicacion
            {
                IdUbicacion = nuevoId,
                Bahia = dto.Bahia,
                Nombre = dto.Nombre,
                Detalle = dto.Detalle
            };

            await _repo.AddAsync(entity);
        }

        public override async Task UpdateAsync(decimal id, Ubicacion dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Ubicación no encontrada");
            }

            if (!string.IsNullOrEmpty(dto.Nombre))
            {
                entity.Nombre = dto.Nombre;
            }

            if (!string.IsNullOrEmpty(dto.Detalle))
            {
                entity.Detalle = dto.Detalle;
            }

            _repo.Update(entity);
        }
    }
}


