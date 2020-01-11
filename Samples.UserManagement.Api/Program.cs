using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Samples.UserManagement.Api.Interfaces;

namespace Samples.UserManagement.Api
{
    public static class Program
    {
        public static void Main()
        {
            var hostBuilder = new HostBuilder().UseContentRoot(Directory.GetCurrentDirectory())
                                               .ConfigureHostConfiguration(BuildConfiguration)
                                               .ConfigureAppConfiguration((wc, conf) => BuildConfiguration(conf))
                                               .ConfigureLogging((x, b) => b.AddConfiguration(x.Configuration.GetSection("Logging"))
                                                                            .AddConsole()
                                                                            .AddDebug()
                                                                            .SetMinimumLevel(LogLevel.Debug))
                                               .ConfigureWebHost(whb => whb.UseShutdownTimeout(TimeSpan.FromSeconds(15))
                                                                           .UseUrls("http://*:8084")
                                                                           .UseKestrel()
                                                                           .UseStartup<ApiStartup>());

            var host = hostBuilder.Build();

            // Background some seed data
            var demoDataService = host.Services.GetRequiredService<IDemoDataService>();
            Task.Run(() => demoDataService.CreateDemoData());

            host.Run();
        }

        // The configuration produced by this method is used for both the host and app configurations.
        private static void BuildConfiguration(IConfigurationBuilder conf)
            => conf.AddJsonFile("appsettings.json", false, true)
                   .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local"}.json", true, true);
    }
}
