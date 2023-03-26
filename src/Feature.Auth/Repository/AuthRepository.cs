using Feature.Auth.Config;
using Feature.Core;
using Feature.Core.AuthUser;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Auth.Repository
{
    public class AuthRepository : FeatureRepositoryBase
    {
        private AuthConfig _config;

        public AuthRepository(AuthConfig config) : base(config)
        {
            _config = config;
        }

        public async Task<User> GetUserAsync(UserLogin userLogin)
        {
            const string SQL_membresia_User_Get = "[membresia].[User_Get]";
            var param = new Dictionary<string, object>();
            param.Add("@UserName", userLogin.UserName);
            param.Add("@Password", userLogin.Password);

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<User>(
                                            commandText: SQL_membresia_User_Get,
                                            commandType: System.Data.CommandType.StoredProcedure
                                            );

                return data.FirstOrDefault();
            }

        }

        public async Task<User> GetUserByUserIdAsync(int userId)
        {
            const string SQL_membresia_Usuario_Obtener = "[membresia].[Usuario_Obtener]";
            var param = new Dictionary<string, object>
            {
                { "@UsuarioId", userId }
            };

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<dynamic>(
                                            commandText: SQL_membresia_Usuario_Obtener,
                                            commandType: System.Data.CommandType.StoredProcedure,
                                            param: param
                                            );
                if (data.Any())
                {
                    var dataUser = data.First();
                    var user = new User()
                    {
                        UserId = dataUser.UsuarioId,
                        UserName = dataUser.NombreUsuario,
                        Name = dataUser.Nombre,
                        LastName = dataUser.Apellido,
                        Email = dataUser.Email,
                        Membresia = new Membresia()
                        {
                            UsuarioId = dataUser.UsuarioId,
                            Salt = dataUser.Salt,
                            Password = dataUser.Password,
                            Verificado = dataUser.Verificado,
                            Verificador = dataUser.Verificador,
                            FechaCaducidad = dataUser.FechaCaducidad,
                            FechaCreacion = dataUser.FechaCreacion,
                            FechaUltimoLogin = dataUser.FechaUltimoLogin,
                            Habilitado = dataUser.Habilitado,
                            Bloqueado = dataUser.Bloqueado,
                            PreguntaSecreta = dataUser.PreguntaSecreta,
                            PreguntaSecretaRespuesta = dataUser.PreguntaSecretaRespuesta,
                            IntentoFallidoLoginCantidad = dataUser.IntentoFallidoLoginCantidad,
                            IntentoFallidoLoginVentana = dataUser.IntentoFallidoLoginVentana,
                            IntentoFallidoPreguntaCantidad = dataUser.IntentoFallidoPreguntaCantidad,
                            IntentoFallidoPreguntaVentana = dataUser.IntentoFallidoPreguntaVentana,
                            UsuarioIdUltimaModificacion = dataUser.UsuarioIdUltimaModificacion
                        },
                        //COMEDOR
                        ComedorId = dataUser.ComedorId,
                        ComedorDescripcion = dataUser.ComedorDescripcion,
                    };
                    return user;
                }
                return null;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            const string SQL_membresia_Usuario_Obtener = "[membresia].[Usuario_Obtener]";
            var param = new Dictionary<string, object>();
            param.Add("@NombreUsuario", username);

            using (var conn = GetConnection())
            {
                var data = await conn.ExecuteQueryAsync<dynamic>(
                                            commandText: SQL_membresia_Usuario_Obtener,
                                            commandType: System.Data.CommandType.StoredProcedure,
                                            param: param
                                            );
                if (data.Any())
                {
                    var dataUser = data.First();
                    var user = new User()
                    {
                        UserId = dataUser.UsuarioId,
                        UserName = dataUser.NombreUsuario,
                        Name = dataUser.Nombre,
                        LastName = dataUser.Apellido,
                        Email = dataUser.Email,
                        Membresia = new Membresia()
                        {
                            UsuarioId = dataUser.UsuarioId,
                            Salt = dataUser.Salt,
                            Password = dataUser.Password,
                            Verificado = dataUser.Verificado,
                            Verificador = dataUser.Verificador,
                            FechaCaducidad = dataUser.FechaCaducidad,
                            FechaCreacion = dataUser.FechaCreacion,
                            FechaUltimoLogin = dataUser.FechaUltimoLogin,
                            Habilitado = dataUser.Habilitado,
                            Bloqueado = dataUser.Bloqueado,
                            PreguntaSecreta = dataUser.PreguntaSecreta,
                            PreguntaSecretaRespuesta = dataUser.PreguntaSecretaRespuesta,
                            IntentoFallidoLoginCantidad = dataUser.IntentoFallidoLoginCantidad,
                            IntentoFallidoLoginVentana = dataUser.IntentoFallidoLoginVentana,
                            IntentoFallidoPreguntaCantidad = dataUser.IntentoFallidoPreguntaCantidad,
                            IntentoFallidoPreguntaVentana = dataUser.IntentoFallidoPreguntaVentana,
                            UsuarioIdUltimaModificacion = dataUser.UsuarioIdUltimaModificacion
                        },
                        //COMEDOR
                        ComedorId= dataUser.ComedorId,
                        ComedorDescripcion= dataUser.ComedorDescripcion,
                    };
                    return user;
                }
                return null;
            }
        }
    }
}
