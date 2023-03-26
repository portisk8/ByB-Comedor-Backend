using Feature.Auth.Config;
using Feature.Auth.Repository;
using Feature.Auth.Util;
using Feature.Core.AuthUser;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Feature.Auth.Business
{
    public class AuthBusiness
    {
        private AuthConfig _authConfig;
        private AuthRepository _authRepository;
        private Serilog.ILogger _logger;

        public AuthBusiness(AuthRepository authRepository,
                             AuthConfig authConfig,
                             Serilog.ILogger logger
                             )
        {
            _authRepository = authRepository;
            _authConfig = authConfig;
            _logger = logger;
        }
        public async Task<UserResponse> LoginAsync(UserLogin userLogin)
        {
            try
            {
                var user = await GetUserByUsernameAsync(userLogin.UserName);
                var isValid = await ValidateAsync(user, userLogin.Password);
                if (!isValid)
                    throw new Exception("Usuario no es válido. Verifique el nombre de usaurio y contraseña");

                var userResponse = new UserResponse()
                {
                    UserName = user.UserName,
                    UserId = user.UserId,
                    Token = GenerateJwt(user)
                };

                return userResponse;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "AuthBusiness > GetUserAsync");
                return null;
            }
        }
        public async Task<User> GetUserAsync(UserLogin userLogin)
        {
            try
            {
                return await _authRepository.GetUserAsync(userLogin);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "AuthBusiness > GetUserAsync");
                return null;
            }
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await _authRepository.GetUserByUsernameAsync(username);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "AuthBusiness > GetUserAsync");
                return null;
            }
        }

        public async Task<bool> ValidarAsync(string nombreUsuario, string password)
        {
            var usuario = await GetUserByUsernameAsync(nombreUsuario);
            return await ValidateAsync(usuario, password);
        }

        #region Private Methods
        private async Task<bool> ValidateAsync(User usuario, string password)
        {
            if (usuario == null)
            {
                return await Task.FromResult(false);
            }

            if (!usuario.Membresia.Verificado
                || usuario.Membresia.Bloqueado
                || !usuario.Membresia.Habilitado)
            {
                return await Task.FromResult(false);
            }

            if (usuario.Membresia.FechaCaducidad.HasValue
                && DateTime.Now >= usuario.Membresia.FechaCaducidad)
            {
                //UsuarioRepository.Bloquear(usuario.UsuarioId);
                return await Task.FromResult(false);
            }

            return await ValidatePasswordAsync(usuario, password);
        }
        private async Task<bool> ValidatePasswordAsync(User usuario, string password)
        {
            if (usuario == null)
            {
                return false;
            }

            bool esValido = PasswordHash.Verify<System.Security.Cryptography.SHA512>(password,
                                                                            usuario.Membresia.Salt,
                                                                            usuario.Membresia.Password);

            return esValido;
        }
        private string GenerateJwt(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_authConfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString()),
                    new Claim("userId", user.UserId.ToString(), ClaimValueTypes.Integer32),
                    new Claim("username", user.UserName, ClaimValueTypes.String),
                    new Claim("name", user.Name, ClaimValueTypes.String),
                    new Claim("lastName", user.LastName, ClaimValueTypes.String),
                    //COMEDOR
                    new Claim("comedorId", user.ComedorId?.ToString(), ClaimValueTypes.Integer32),
                    new Claim("comedorDescripcion", user.ComedorDescripcion, ClaimValueTypes.String)

                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<UserResponse> RefreshTokenAsync(int userId)
        {
            var user = await _authRepository.GetUserByUserIdAsync(userId);

            var userResponse = new UserResponse()
            {
                UserName = user.UserName,
                UserId = user.UserId,
                Token = GenerateJwt(user)
            };

            return userResponse;
        }
        #endregion
    }
}
