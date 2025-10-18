using Api.Controllers.Template;
using Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ParametroController : BaseController<Parametro>
    {
        public ParametroController(IParametroService service) : base(service) { }
    }
}