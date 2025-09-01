using Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers.Template;
using Domain.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class RolController : BaseController<RolDto>
    {
        public RolController(IRolService service) : base(service) { }
    }
}
