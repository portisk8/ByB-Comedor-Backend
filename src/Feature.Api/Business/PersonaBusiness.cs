using Feature.Api.Config;
using Feature.Api.Entities;
using Feature.Api.Entities.DTOs;
using Feature.Api.Entities.Filtros;
using Feature.Api.Repository;
using Feature.Core.Entities;

namespace Feature.Api.Business
{
    public class PersonaBusiness
    {
        private ApiConfig _apiConfig;
        private PersonaRepository _personaRepository;
        private Serilog.ILogger _logger;

        public PersonaBusiness(PersonaRepository personaRepository,
                             ApiConfig apiConfig,
                             Serilog.ILogger logger
                             )
        {
            _personaRepository = personaRepository;
            _apiConfig = apiConfig;
            _logger = logger;
        }

        public async Task<List<Persona>> PersonaBuscarAsync(PersonaFiltro filtro)
        {
            return await _personaRepository.PersonaBuscarAsync(filtro);
        }

        public async Task<GenericResponse> PersonaGuardarAsync(PersonaDTO dto)
        {
            return await _personaRepository.PersonaGuardarAsync(dto);
        }

        public async Task<List<Persona>> PersonaListarAsync(PersonaFiltro filtro)
        {
            return await _personaRepository.PersonaListarAsync(filtro);
        }

        public async Task<Persona> PersonaObtenerAsync(int personaId)
        {
            return await _personaRepository.PersonaObtenerAsync(personaId);
        }

        public async Task<List<PersonaHistorial>> PersonaHistorialBuscarAsync(PersonaFiltro filtro)
        {
            return await _personaRepository.PersonaHistorialBuscarAsync(filtro);
        }
    }
}
