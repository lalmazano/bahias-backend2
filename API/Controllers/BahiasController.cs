using Api.Controllers.Template;
using Application.Services.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class BahiasController : BaseController<Bahium>
    {
        public BahiasController(IBahiaService service) : base(service) { }
    }
}