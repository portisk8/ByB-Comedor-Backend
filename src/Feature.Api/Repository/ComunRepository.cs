using Feature.Api.Config;
using Feature.Api.Entities;
using Feature.Api.Entities.Filtros;
using Feature.Core;
using RepoDb;

namespace Feature.Api.Repository
{
    public class ComunRepository : FeatureRepositoryBase
    {
        private ApiConfig _config;

        public ComunRepository(ApiConfig config) : base(config)
        {
            _config = config;
        }
        public async Task<List<DocumentoTipo>> DocumentoTipoListarAsync()
        {

            const string SQL_DocumentoTipo_Listar = "[dbo].[DocumentoTipo_Listar]";

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<DocumentoTipo>(
                                            commandText: SQL_DocumentoTipo_Listar,
                                            commandType: System.Data.CommandType.StoredProcedure
                                            );

                return data.ToList();
            }
        }
        public async Task<List<SexoTipo>> SexoTipoListarAsync()
        {

            const string SQL_SexoTipo_Listar = "[dbo].[SexoTipo_Listar]";

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<SexoTipo>(
                                            commandText: SQL_SexoTipo_Listar,
                                            commandType: System.Data.CommandType.StoredProcedure
                                            );

                return data.ToList();
            }
        }
    }
}
