using Feature.Api.Config;
using Feature.Api.Entities.Filtros;
using Feature.Api.Entities;
using Feature.Core;
using Feature.Core.Config;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feature.Core.Entities;
using Feature.Api.Entities.DTOs;

namespace Feature.Api.Repository
{
    public class PersonaRepository : FeatureRepositoryBase
    {
        private ApiConfig _config;

        public PersonaRepository(ApiConfig config) : base(config)
        {
            _config = config;
        }
        public async Task<List<Persona>> PersonaBuscarAsync(PersonaFiltro filtro)
        {
            const string SQL_Persona_Buscar = "[dbo].[Persona_Buscar]";
            var param = new Dictionary<string, object?>
            {
                { "@EsInfante", filtro.EsInfante },
                { "@ComedorId", filtro.ComedorId },
                { "@PalabrasABuscar", filtro.PalabrasABuscar },
                { "@ColumnaAOrdenar", filtro.ColumnaAOrdenar },
                { "@PageIndex", filtro.PageIndex },
                { "@PageSize", filtro.PageSize }
            };

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<Persona>(
                                            commandText: SQL_Persona_Buscar,
                                            commandType: System.Data.CommandType.StoredProcedure,
                                            param: param
                                            );

                return data.ToList();
            }
        }

        public async Task<GenericResponse?> PersonaGuardarAsync(PersonaDTO dto)
        {
            const string SQL_Persona_Guardar = "[dbo].[Persona_Guardar]";
            var param = new Dictionary<string, object?>
            {
                { "@PersonaId", dto.PersonaId },
                { "@Nombre", dto.Nombre },
                { "@Apellido", dto.Apellido },
                { "@FechaNacimiento", dto.FechaNacimiento },
                { "@DocumentoTipoId", dto.DocumentoTipoId },
                { "@Documento", dto.Documento },
                { "@SexoTipoId", dto.SexoTipoId },
                { "@ComedorId", dto.ComedorId },
                { "@Edad", dto.Edad },
                { "@DireccionCalle", dto.DireccionCalle },
                { "@DireccionNumero", dto.DireccionNumero },
                { "@PersonaTutorId", dto.PersonaTutorId },
                { "@TelefonoNumero", dto.TelefonoNumero },
                { "@UsuarioId", dto.UsuarioId }
            };

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<GenericResponse>(
                                            commandText: SQL_Persona_Guardar,
                                            commandType: System.Data.CommandType.StoredProcedure,
                                            param: param
                                            );

                return data.FirstOrDefault();
            }
        }

        public async Task<List<Persona>> PersonaListarAsync(PersonaFiltro filtro)
        {
            const string SQL_Persona_Listar = "[dbo].[Persona_Listar]";
            var param = new Dictionary<string, object?>
            {
                { "@EsInfante", filtro.EsInfante },
                { "@ComedorId", filtro.ComedorId }
            };

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<Persona>(
                                            commandText: SQL_Persona_Listar,
                                            commandType: System.Data.CommandType.StoredProcedure,
                                            param: param
                                            );

                return data.ToList();
            }
        }

        public async Task<Persona> PersonaObtenerAsync(int personaId)
        {
            const string SQL_Persona_Obtener = "[dbo].[Persona_Obtener]";
            var param = new Dictionary<string, object?>
            {
                { "@PersonaId", personaId }
            };

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<Persona>(
                                            commandText: SQL_Persona_Obtener,
                                            commandType: System.Data.CommandType.StoredProcedure,
                                            param: param
                                            );

                return data.FirstOrDefault();
            }
        }

        public async Task<List<PersonaHistorial>> PersonaHistorialBuscarAsync(PersonaFiltro filtro)
        {
            const string SQL_PersonaHistorial_Buscar = "[dbo].[PersonaHistorial_Buscar]";
            var param = new Dictionary<string, object?>
            {
                { "@PersonaId", filtro.PersonaId },
                { "@FechaDesde", filtro.FechaDesde },
                { "@FechaHasta", filtro.FechaHasta },
                { "@PalabrasABuscar", filtro.PalabrasABuscar },
                { "@ColumnaAOrdenar", filtro.ColumnaAOrdenar },
                { "@PageIndex", filtro.PageIndex },
                { "@PageSize", filtro.PageSize }
            };

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<PersonaHistorial>(
                                            commandText: SQL_PersonaHistorial_Buscar,
                                            commandType: System.Data.CommandType.StoredProcedure,
                                            param: param
                                            );

                return data.ToList();
            }
        }
    }
}
