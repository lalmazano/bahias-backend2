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
    public class VehiculoService : ServiceBase<Vehiculo, Vehiculo>, IVehiculoService
    {
        private readonly IVehiculoRepository _repo;

        public VehiculoService(IVehiculoRepository repo, IMapper mapper)
            : base(repo, mapper)
        {
            _repo = repo;
        }

        public override async Task AddAsync(Vehiculo dto)
        {
            var ultimo = await _repo.GetLastAsync(u => u.IdVehiculo);
            var nuevoId = (ultimo?.IdVehiculo ?? 0) + 1;

            var entity = new Vehiculo
            {
                IdVehiculo = nuevoId,
                Placa = dto.Placa,
                Modelo = dto.Modelo,
                Marca = dto.Marca,
                Color = dto.Color
            };

            await _repo.AddAsync(entity);
        }

        public override async Task UpdateAsync(decimal id, Vehiculo dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Vehículo no encontrado");
            }

            if (!string.IsNullOrEmpty(dto.Placa))
            {
                entity.Placa = dto.Placa;
            }

            if (!string.IsNullOrEmpty(dto.Modelo))
            {
                entity.Modelo = dto.Modelo;
            }

            if (!string.IsNullOrEmpty(dto.Marca))
            {
                entity.Marca = dto.Marca;
            }

            if (!string.IsNullOrEmpty(dto.Color))
            {
                entity.Color = dto.Color;
            }

            _repo.Update(entity);
        }
    }
}