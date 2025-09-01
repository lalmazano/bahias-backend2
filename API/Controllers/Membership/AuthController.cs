using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Services.Interface;
using Domain.DTOs;
using Shared.Security.JWT.Interface;

namespace Srv_inventory.Controllers.Membership
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IJwtService _jwtService;
        private readonly IUserRolService _userRolService;


        public LoginController(IUsuarioService usuarioService, IJwtService jwtService, IUserRolService UserRolService)
        {
            _usuarioService = usuarioService;
            _jwtService = jwtService;
            _userRolService = UserRolService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto loginDto)
        {
            var isValid = await _usuarioService.VerifyPasswordAsync(loginDto);

            if (!isValid)
                return Unauthorized(new { Message = "Usuario o contraseña inválidos" });
            // si el usuario esta inactivo
            var usuario = await _usuarioService.GetByUsernameAsync(loginDto.Username);
            if (usuario == null || !usuario.Estado.Equals("A"))
                return Unauthorized(new { Message = "Usuario inactivo o no encontrado" });

            var roles = await _userRolService.GetRolesByUserId(loginDto.Username);
            var nombresroles = roles.Select(x => x.Name).ToList();
            var nombre = await _usuarioService.GetByUsernameAsync(loginDto.Username);
            var token = _jwtService.GenerateToken(loginDto.Username);

            return Ok(new 
            {
                username = loginDto.Username,
                roles = nombresroles,
                Token = token,
                nombre = nombre.Nombre + " " + nombre.Apellido

            });
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRolesByUserId()
        {
            var username = User.Identity.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized("No se Pudo identificar el usuario");

            var roles = await _userRolService.GetRolesByUserId(username);
            if (roles == null || !roles.Any())
            {
                return NotFound(new { message = "No se encontraron roles para este usuario." });
            }

            var nombresroles = roles.Select(x => x.Name).ToList();

            return Ok(nombresroles);
        }


    }
}