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
    public class BahiaService : ServiceBase<Bahium, Bahium>, IBahiaService
    {
        private readonly IBahiasRepository _repo;

        public BahiaService(IBahiasRepository repo, IMapper mapper)
            : base(repo, mapper)
        {
            _repo = repo;
        }

        public override async Task AddAsync(Bahium dto)
        {
            var ultimo = await _repo.GetLastAsync(u => u.IdBahia);
            var nuevoId = (ultimo?.IdBahia ?? 0) + 1;

            var entity = new Bahium
            {
                IdBahia = nuevoId,
            };

            await _repo.AddAsync(entity);
        }

        public override async Task UpdateAsync(decimal id, Bahium dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Bahia no encontrada");
            }

            // Aquí puedes agregar las actualizaciones necesarias para la entidad Bahium
            // Por ejemplo:
            // if (!string.IsNullOrEmpty(dto.AlgunaPropiedad))
            // {
            //     entity.AlgunaPropiedad = dto.AlgunaPropiedad;
            // }

            _repo.Update(entity);
        }

    }
}


