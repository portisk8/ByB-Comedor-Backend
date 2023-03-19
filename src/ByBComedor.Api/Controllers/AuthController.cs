using Feature.Auth.Business;
using Microsoft.AspNetCore.Mvc;

namespace ByBComedor.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Feature.Auth.Controllers.AuthControllerInternal
    {
        public AuthController(AuthBusiness authBusiness, 
                                Serilog.ILogger logger) 
            : base(authBusiness, logger)
        {
        }

    }
}
