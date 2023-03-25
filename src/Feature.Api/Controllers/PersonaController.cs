using Feature.Api.Business;
using Feature.Api.Entities.DTOs;
using Feature.Api.Entities.Filtros;
using Feature.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Feature.Api.Controllers
{
    [ApiController]
    public class PersonaControllerInternal : CoreAuthControllerBase
    {
        private PersonaBusiness _personaBusiness { get; set; }

        private ILogger _logger { get; set; }

        public PersonaControllerInternal(PersonaBusiness personaBusiness,
                                ILogger logger)
        {
            _personaBusiness = personaBusiness;
            _logger = logger;
        }

        [HttpPost]
        [Route("buscar")]
        public async Task<IActionResult> PersonaBuscar([FromBody] PersonaFiltro filtro)
        {
            try
            {
                var resultado = await _personaBusiness.PersonaBuscarAsync(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error($"[PersonaController] PersonaBuscar > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("guardar")]
        public async Task<IActionResult> PersonaGuardar([FromBody] PersonaDTO dto)
        {
            try
            {
                dto.UsuarioId = CurrentUser.UserId;
                var resultado = await _personaBusiness.PersonaGuardarAsync(dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error($"[PersonaController] PersonaGuardar > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("listar")]
        public async Task<IActionResult> PersonaListar([FromBody] PersonaFiltro filtro)
        {
            try
            {
                var resultado = await _personaBusiness.PersonaListarAsync(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error($"[PersonaController] PersonaListar > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{personaId}/obtener")]
        public async Task<IActionResult> PersonaObtener(int personaId)
        {
            try
            {
                var resultado = await _personaBusiness.PersonaObtenerAsync(personaId);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error($"[PersonaController] PersonaObtener > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("historial/buscar")]
        public async Task<IActionResult> PersonaHistorialBuscar([FromBody] PersonaFiltro filtro)
        {
            try
            {
                var resultado = await _personaBusiness.PersonaHistorialBuscarAsync(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error($"[PersonaController] PersonaHistorialBuscar > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("historial/guardar")]
        public async Task<IActionResult> PersonaHistorialGuardar([FromBody] PersonaHistorialDTO dto)
        {
            try
            {
                var resultado = await _personaBusiness.PersonaHistorialGuardarAsync(dto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.Error($"[PersonaController] PersonaHistorialGuardar > {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

    }
}
