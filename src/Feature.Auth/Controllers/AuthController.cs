using Feature.Auth.Business;
using Feature.Core.AuthUser;
using Feature.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Auth.Controllers
{
    [ApiController]
    public class AuthControllerInternal: CoreAuthControllerBase
    {
        private AuthBusiness _authBusiness{ get; set; }

        private ILogger _logger { get; set; }

        public AuthControllerInternal(AuthBusiness authBusiness,
                                ILogger logger)
        {
            _authBusiness = authBusiness;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var resultado = await _authBusiness.LoginAsync(userLogin);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error($"[AuthController] Login > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("test")]
        public async Task<IActionResult> Test()
        {
            try
            {
                return Ok("TEST OK");
            }
            catch (Exception ex)
            {
                _logger.Error($"[AuthController] Test > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> Me()
        {
            try
            {
                return Ok(CurrentUser);
            }
            catch (Exception ex)
            {
                _logger.Error($"[AuthController] Test > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
