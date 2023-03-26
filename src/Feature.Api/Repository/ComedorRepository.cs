using Feature.Api.Config;
using Feature.Api.Entities;
using Feature.Api.Entities.Filtros;
using Feature.Core;
using Feature.Core.Entities;
using RepoDb;

namespace Feature.Api.Repository
{
    public class ComedorRepository : FeatureRepositoryBase
    {
        private ApiConfig _config;

        public ComedorRepository(ApiConfig config) : base(config)
        {
            _config = config;
        }
        public async Task<List<Comedor>> ComedorBuscarASync(ComedorFiltro filtro)
        {

            const string SQL_Comedor_Buscar = "[dbo].[Comedor_Buscar]";
            var param = new Dictionary<string, object>
            {
                { "@UsuarioId", filtro.UsuarioId },
                { "@PalabrasABuscar", filtro.PalabrasABuscar },
                { "@ColumnaAOrdenar", filtro.ColumnaAOrdenar },
                { "@PageIndex", filtro.PageIndex },
                { "@PageSize", filtro.PageSize }
            };

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<Comedor>(
                                            commandText: SQL_Comedor_Buscar,
                                            commandType: System.Data.CommandType.StoredProcedure,
                                            param: param
                                            );

                return data.ToList();
            }
        }

        public async Task<GenericResponse> ComedorCambiarAsync(int comedorId, int userId)
        {
            const string SQL_UsuarioPropiedadValor_Guardar = "[perfil].[UsuarioPropiedadValor_Guardar]";
            var param = new Dictionary<string, object>
            {
                { "@PropiedadNombre", "EntidadIdActual" },
                { "@UsuarioId", userId },
                { "@Valor", comedorId }
            };

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<GenericResponse>(
                                            commandText: SQL_UsuarioPropiedadValor_Guardar,
                                            commandType: System.Data.CommandType.StoredProcedure,
                                            param: param
                                            );

                return data.FirstOrDefault();
            }
        }
        
    }
}
