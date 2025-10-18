using Api.Controllers.Template;
using Application.Services.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class VehiculoController : BaseController<Vehiculo>
    {
        public VehiculoController(IVehiculoService service) : base(service) { }
    }
}
