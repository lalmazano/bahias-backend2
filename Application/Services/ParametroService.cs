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
    public class ParametroService : ServiceBase<Parametro, Parametro>, IParametroService
    {
        private readonly IParametroRepository _repo;

        public ParametroService(IParametroRepository repo, IMapper mapper)
            : base(repo, mapper)
        {
            _repo = repo;
        }

        public override async Task AddAsync(Parametro dto)
        {
            var ultimo = await _repo.GetLastAsync(u => u.IdParametro);
            var nuevoId = (ultimo?.IdParametro ?? 0) + 1;

            var entity = new Parametro
            {
                IdParametro = nuevoId,
                Clave = dto.Clave,
                Valor = dto.Valor,
                Descripcion = dto.Descripcion
            };

            await _repo.AddAsync(entity);
        }

        public override async Task UpdateAsync(decimal id, Parametro dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("Parámetro no encontrado");
            }

            if (!string.IsNullOrEmpty(dto.Clave))
            {
                entity.Clave = dto.Clave;
            }

            if (!string.IsNullOrEmpty(dto.Valor))
            {
                entity.Valor = dto.Valor;
            }

            if (!string.IsNullOrEmpty(dto.Descripcion))
            {
                entity.Descripcion = dto.Descripcion;
            }

            _repo.Update(entity);
        }
    }
}