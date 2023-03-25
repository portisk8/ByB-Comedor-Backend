using Feature.Api.Business;
using Feature.Api.Entities.Filtros;
using Feature.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Feature.Api.Controllers
{
    [ApiController]
    public class ComedorControllerInternal : CoreAuthControllerBase
    {
        private ComedorBusiness _comedorBusiness { get; set; }

        private ILogger _logger { get; set; }

        public ComedorControllerInternal(ComedorBusiness comedorBusiness,
                                ILogger logger)
        {
            _comedorBusiness = comedorBusiness;
            _logger = logger;
        }

        [HttpPost]
        [Route("buscar")]
        public async Task<IActionResult> ComedorBuscar([FromBody] ComedorFiltro filtro)
        {
            try
            {
                var resultado = await _comedorBusiness.ComedorBuscarASync(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error($"[ComedorController] ComedorBuscar > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        
    }
}
