using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Feature.Auth.Authentication
{
    public class AuthenticationHandler : AuthenticationHandler<AuthenticationOptions>
    {

        public AuthenticationHandler(
            IOptionsMonitor<AuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Unauthorized");

            var authHeaders = Request.Headers["Authorization"].ToString().Split(" ");

            if (authHeaders[0] != "inb")
                return AuthenticateResult.Fail("Unauthorized");

            var base64EncodedBytes = Convert.FromBase64String(authHeaders[1]);
            var decodedBase64 = Encoding.UTF8.GetString(base64EncodedBytes).Split("|");

            var userId = Convert.ToInt64(decodedBase64[0]);

            var claims = new List<Claim>();

            claims.Add(new Claim("userId", userId.ToString()));

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

    }
}
