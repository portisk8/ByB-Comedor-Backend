using Feature.Api.Config;
using Feature.Api.Entities;
using Feature.Api.Entities.Filtros;
using Feature.Api.Repository;
using Feature.Core.AuthUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Api.Business
{
    public class ComunBusiness
    {
        private ApiConfig _apiConfig;
        private ComunRepository _comunRepository;
        private Serilog.ILogger _logger;

        public ComunBusiness(ComunRepository comunRepository,
                             ApiConfig apiConfig,
                             Serilog.ILogger logger
                             )
        {
            _comunRepository = comunRepository;
            _apiConfig = apiConfig;
            _logger = logger;
        }

        public async Task<List<DocumentoTipo>> DocumentoTipoListarAsync()
        {
            return await _comunRepository.DocumentoTipoListarAsync();
        }
        public async Task<List<SexoTipo>> SexoTipoListarAsync()
        {
            return await _comunRepository.SexoTipoListarAsync();
        }
    }
}
