using Feature.Auth.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Auth.Middleware
{
    public class JwtMiddleware
    {
        private const string USERNAME_KEY = "Username";
        private const string WHITESPACE = " ";
        private const string HTTP_HEADER_AUTHORIZATION = "Authorization";
        private AuthConfig _authConfig;
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next, AuthConfig authConfig)
        {
            _next = next;
            _authConfig = authConfig;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers[HTTP_HEADER_AUTHORIZATION].FirstOrDefault()?.Split(WHITESPACE).Last();

            if (token != null && token != "null")
                await AttachToContextAsync(context, token);

            await _next(context);
        }

        private async Task AttachToContextAsync(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes(_authConfig.SecretKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var username = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var userId = long.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);
                
                context.Items[USERNAME_KEY] = username;
            }
            catch (Exception ex)
            {
                //TODO: Log
            }
        }
    }
}