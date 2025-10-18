using Api.Controllers.Template;
using Application.Services.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UbicacionController : BaseController<Ubicacion>
    {
        public UbicacionController(IUbicacionService service) : base(service) { }
    }
}
