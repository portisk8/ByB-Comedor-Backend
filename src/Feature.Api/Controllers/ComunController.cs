using Feature.Api.Business;
using Feature.Api.Entities.Filtros;
using Feature.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Feature.Api.Controllers
{
    public class ComunControllerInternal : CoreAuthControllerBase
    {
        private ComunBusiness _comunBusiness { get; set; }

        private ILogger _logger { get; set; }

        public ComunControllerInternal(ComunBusiness comunBusiness,
                                ILogger logger)
        {
            _comunBusiness = comunBusiness;
            _logger = logger;
        }

        [HttpGet]
        [Route("documentoTipo/listar")]
        public async Task<IActionResult> DocumentoTipoListar()
        {
            try
            {
                var resultado = await _comunBusiness.DocumentoTipoListarAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error($"[ComunController] DocumentoTipoListar > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("sexoTipo/listar")]
        public async Task<IActionResult> SexoTipoListar()
        {
            try
            {
                var resultado = await _comunBusiness.SexoTipoListarAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error($"[ComunController] SexoTipoListar > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
