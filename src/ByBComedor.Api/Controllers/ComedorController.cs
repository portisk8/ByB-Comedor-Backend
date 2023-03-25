using Feature.Api.Business;
using Feature.Auth.Business;
using Microsoft.AspNetCore.Mvc;

namespace ByBComedor.Api.Controllers
{
    [Route("api/[controller]")]
    public class ComedorController : Feature.Api.Controllers.ComedorControllerInternal
    {
        public ComedorController(ComedorBusiness comedorBusiness, 
                                Serilog.ILogger logger) 
            : base(comedorBusiness, logger)
        {
        }

    }
}
