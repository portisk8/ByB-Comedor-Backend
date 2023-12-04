using Feature.Api.Business;
using Feature.Api.Entities.DTOs;
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

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> ComedorListar()
        {
            var filtro = new ComedorFiltro()
            {
                PageIndex = 1,
                PageSize = Int32.MaxValue,
                UsuarioId = CurrentUser.UserId
            };
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

        [HttpGet]
        [Route("{comedorId}/cambiar")]
        public async Task<IActionResult> ComedorCambiar(int comedorId)
        {
            try
            {
                var resultado = await _comedorBusiness.ComedorCambiarAsync(comedorId, CurrentUser.UserId);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error($"[ComedorController] ComedorBuscar > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("guardar")]
        public async Task<IActionResult> ComedorGuardar([FromBody] ComedorDTO dto)
        {
            try
            {
                var resultado = await _comedorBusiness.ComedorGuardarAsync(dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error($"[ComedorController] ComedorGuardar > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

    }
}
