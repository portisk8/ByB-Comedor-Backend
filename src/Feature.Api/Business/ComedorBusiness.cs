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
    public class ComedorBusiness
    {
        private ApiConfig _apiConfig;
        private ComedorRepository _comedorRepository;
        private Serilog.ILogger _logger;

        public ComedorBusiness(ComedorRepository comedorRepository,
                             ApiConfig apiConfig,
                             Serilog.ILogger logger
                             )
        {
            _comedorRepository = comedorRepository;
            _apiConfig = apiConfig;
            _logger = logger;
        }

        public async Task<List<Comedor>> ComedorBuscarASync(ComedorFiltro filtro)
        {
            return await _comedorRepository.ComedorBuscarASync(filtro);
        }
    }
}
