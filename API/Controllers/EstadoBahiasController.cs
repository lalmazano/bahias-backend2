using Api.Controllers.Template;
using Application.Services.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    public class EstadoBahiasController : BaseController<EstadoBahium>
    {
        public EstadoBahiasController(IEstadoBahiasService service) : base(service) { }
    }
}
