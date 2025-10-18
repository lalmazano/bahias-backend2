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
    public class ReservaService : ServiceBase<Reserva, Reserva>, IReservaService
    {
        private readonly IReservaRepository _repo;

        public ReservaService(IReservaRepository repo, IMapper mapper)
            : base(repo, mapper)
        {
            _repo = repo;
        }

        public override async Task AddAsync(Reserva dto)
        {
            var ultimo = await _repo.GetLastAsync(u => u.IdReserva);
            var nuevoId = (ultimo?.IdReserva ?? 0) + 1;

            var entity = new Reserva
            {
                IdReserva = nuevoId,

            };

            await _repo.AddAsync(entity);
        }

        public override async Task UpdateAsync(decimal id, Reserva dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Reserva no encontrada");
            }

            // Aquí puedes agregar las actualizaciones necesarias para la entidad Reserva
            // Por ejemplo:
            // if (!string.IsNullOrEmpty(dto.AlgunaPropiedad))
            // {
            //     entity.AlgunaPropiedad = dto.AlgunaPropiedad;
            // }

            _repo.Update(entity);
        }
    }
}