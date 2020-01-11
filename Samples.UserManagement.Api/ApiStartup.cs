using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Samples.UserManagement.Api.DataAccess;
using Samples.UserManagement.Api.Filters;
using Samples.UserManagement.Api.Interfaces;
using Samples.UserManagement.Api.Services;

namespace Samples.UserManagement.Api
{
    public class ApiStartup
    {
        private readonly IConfiguration _configuration;

        public ApiStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(o =>
                                       {
                                           o.DefaultAuthenticateScheme = ApiKeyAuthSchemeOptions.DefaultScheme;
                                           o.DefaultChallengeScheme = ApiKeyAuthSchemeOptions.DefaultScheme;
                                       })
                    .AddScheme<ApiKeyAuthSchemeOptions, ApiKeyAuthSchemeHandler>(ApiKeyAuthSchemeOptions.DefaultScheme, o => { });

            services.AddMvc(o =>
                            {
                                o.Filters.Add(new ModelAttributeValidationFilter());
                            })
                    .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddSingleton<SubscriptionGetValidationFilter>();

            // Services
            services.AddSingleton<IDemoDataService, DemoDataService>();
            services.AddSingleton<ISequenceProvider, InMemorySequenceProvider>();

            // Repos
            services.AddSingleton<IUserRepository, InMemoryUserRepository>();
            services.AddSingleton<IServiceRepository, InMemoryServiceRepository>();
            services.AddSingleton<ISubscriptionRepository, InMemorySubscriptionRepository>();
            services.AddSingleton<IApiKeyRepository, InMemoryApiKeyRepository>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

    public class ApiKeyAuthSchemeOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "API Key";
        public string Scheme => DefaultScheme;
        public string AuthenticationType = DefaultScheme;
    }
}
