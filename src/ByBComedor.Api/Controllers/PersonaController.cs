using Feature.Api.Business;
using Feature.Auth.Business;
using Microsoft.AspNetCore.Mvc;

namespace ByBComedor.Api.Controllers
{
    [Route("api/[controller]")]
    public class PersonaController : Feature.Api.Controllers.PersonaControllerInternal
    {
        public PersonaController(PersonaBusiness personaBusiness, 
                                Serilog.ILogger logger) 
            : base(personaBusiness, logger)
        {
        }

    }
}
