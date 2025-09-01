using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Template;

namespace Api.Controllers.Template
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TDto> : ControllerBase
    {
        private readonly IServiceBase<TDto> _service;

        public BaseController(IServiceBase<TDto> service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TDto dto)
        {
            await _service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = GetId(dto) }, dto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(decimal id, [FromBody] TDto dto)
        {
            try
            {
                await _service.UpdateAsync(id, dto);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message });
            }
        }

 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(decimal id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // Este método se puede sobrescribir en controladores concretos si necesitás obtener el ID del DTO
        protected virtual decimal GetId(TDto dto) => 0;
    }
}
