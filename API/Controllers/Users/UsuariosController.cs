using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Interface;
using Domain.DTOs;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null) return NotFound(new { Message = "Usuario no encontrado" });
            return Ok(usuario);
        }
        [AllowAnonymous]
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var usuario = await _usuarioService.GetByUsernameAsync(username);
            if (usuario == null) return NotFound(new { Message = "Usuario no encontrado" });
            return Ok(usuario);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UsuarioCreateDto usuarioCreateDto)
        {
            await _usuarioService.AddAsync(usuarioCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = usuarioCreateDto.Username }, usuarioCreateDto);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(decimal id, [FromBody] UsuarioUpdateDto usuarioDto)
        {
            try
            {
                await _usuarioService.UpdateAsync(id, usuarioDto); // Pasa el ID desde la URL
                return Ok(usuarioDto); // Retorna 204 si todo fue exitoso
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message }); // Si el usuario no fue encontrado
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(decimal id)
        {
            await _usuarioService.DeleteAsync(id);
            return NoContent();
        }
    }
}
