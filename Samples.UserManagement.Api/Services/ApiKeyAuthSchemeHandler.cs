using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Samples.UserManagement.Api.DataAccess;
using Samples.UserManagement.Api.Extensions;

namespace Samples.UserManagement.Api.Services
{
    public class ApiKeyAuthSchemeHandler : AuthenticationHandler<ApiKeyAuthSchemeOptions>
    {
        private const string _apiKeyHeader = "X-Api-Key";

        private readonly IApiKeyRepository _apiKeyRepository;

        public ApiKeyAuthSchemeHandler(IOptionsMonitor<ApiKeyAuthSchemeOptions> options,
                                       ILoggerFactory logger,
                                       UrlEncoder encoder,
                                       ISystemClock clock,
                                       IApiKeyRepository apiKeyRepository)
            : base(options, logger, encoder, clock)
        {
            _apiKeyRepository = apiKeyRepository;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(_apiKeyHeader, out var apiKeyValues))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var apiKey = apiKeyValues.FirstOrDefault();

            if (apiKey.IsNullOrEmpty())
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var existingApiKey = _apiKeyRepository.GetById(apiKey);

            if (existingApiKey == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid credentials"));
            }

            var claims = new List<Claim>
                         {
                             new Claim(ClaimTypes.Name, existingApiKey.FriendlyName)
                         };

            if (existingApiKey.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            var idents = new List<ClaimsIdentity>
                         {
                             new ClaimsIdentity(claims, Options.AuthenticationType)
                         };

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(idents), Options.Scheme)));
        }
    }
}
