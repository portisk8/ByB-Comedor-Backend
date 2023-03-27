using Feature.Api.Business;
using Feature.Auth.Business;
using Microsoft.AspNetCore.Mvc;

namespace ByBComedor.Api.Controllers
{
    [Route("api/[controller]")]
    public class ComunController : Feature.Api.Controllers.ComunControllerInternal
    {
        public ComunController(ComunBusiness comunBusiness, 
                                Serilog.ILogger logger) 
            : base(comunBusiness, logger)
        {
        }

    }
}
