using Api.Controllers.Template;
using Application.Services.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class TipoVehiculoController : BaseController<TipoVehiculo>
    {
        public TipoVehiculoController(ITipoVehiculoService service) : base(service) { }
    }
}