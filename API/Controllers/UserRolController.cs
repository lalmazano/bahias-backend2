using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Interface;
using Domain.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolController : ControllerBase
    {
        private readonly IUserRolService _IUserRolService;

        public UserRolController(IUserRolService IUserRolService)
        {
            _IUserRolService = IUserRolService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userolls = await _IUserRolService.GetAllAsync();
            return Ok(userolls);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(decimal id)
        {
            var userolls = await _IUserRolService.GetByIdAsync(id);
            if (userolls == null) return NotFound(new { Message = "Roles no encontrado" });
            return Ok(userolls);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRolDto userolls)
        {
            await _IUserRolService.AddAsync(userolls);
            return CreatedAtAction(nameof(GetById), new { id = userolls.idRol}, userolls);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(decimal id, [FromBody] UserRolDto userolls)
        {
            try
            {
                await _IUserRolService.UpdateAsync(id, userolls); // Pasa el ID desde la URL
                return Ok(userolls); // Retorna 204 si todo fue exitoso
            }
            catch (Exception ex)
            {
                return NotFound(new { ex.Message }); // Si el tipo de usuario no fue encontrado
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(decimal id)
        {
            await _IUserRolService.DeleteAsync(id);
            return NoContent();
        }

        
        [HttpGet("usuario")]
        public async Task<IActionResult> GetRolesByUserId()
        {
            var username = User.Identity.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized("No se Pudo identificar el usuario");

            var roles = await _IUserRolService.GetRolesByUserId(username);
            if (roles == null || !roles.Any())
            {
                return NotFound(new { message = "No se encontraron roles para este usuario." });
            }

            return Ok(roles);
        }

    }
}