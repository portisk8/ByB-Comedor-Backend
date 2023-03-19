using Feature.Auth.Config;
using Feature.Core;
using Feature.Core.AuthUser;
using Feature.Core.Entities;
using RepoDb;
using PropertyValue = Feature.Core.AuthUser.PropertyValue;

namespace Feature.Auth.Repository
{
    public class PerfilRepository : FeatureRepositoryBase
    {
        private AuthConfig _config;

        public PerfilRepository(AuthConfig config) : base(config)
        {
            _config = config;
        }

        public async Task<GenericResponse> CrearAsync(PropertyValue usuarioPropiedadValor)
        {

            const string SQL_UsuarioPropiedadValor_Guardar = "[perfil].[UsuarioPropiedadValor_Guardar]";
            var param = new Dictionary<string, object>
            {
                { "@PropiedadNombre", usuarioPropiedadValor.Nombre },
                { "@UsuarioId", usuarioPropiedadValor.UsuarioId },
                { "@Valor", usuarioPropiedadValor.Valor }
            };

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<GenericResponse>(
                                            commandText: SQL_UsuarioPropiedadValor_Guardar,
                                            commandType: System.Data.CommandType.StoredProcedure
                                            );

                return data.FirstOrDefault();
            }
        }
        public async Task<PropertyValue> ObtenerAsync(int usuarioId, int propiedadId)
        {

            const string SQL_UsuarioPropiedadValor_Obtener = "[perfil].[UsuarioPropiedadValor_Obtener]";
            var param = new Dictionary<string, object>
            {
                { "@PropiedadId", propiedadId },
                { "@UsuarioId", usuarioId }
            };

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<PropertyValue>(
                                            commandText: SQL_UsuarioPropiedadValor_Obtener,
                                            commandType: System.Data.CommandType.StoredProcedure
                                            );

                return data.FirstOrDefault();
            }
        }
    }
}
