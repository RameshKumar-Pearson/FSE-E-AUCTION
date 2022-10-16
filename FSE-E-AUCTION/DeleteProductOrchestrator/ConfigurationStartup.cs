using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EauctionDeleteEventHandler
{
    [ExcludeFromCodeCoverage]
    internal static class ConfigurationStartup
    {
        /// <summary>
        /// Adds configuration providers to allow the application to read configuration from appropriate sources based
        /// on the host environment.
        /// </summary>
        /// <param name="services">Interface for the collection in which services are registered for DI.</param>
        /// <returns>Interface for accessing settings from the application's configuration provider.</returns>
        public static IConfiguration AddConfigurationProviders(IServiceCollection services)
        {
            // Create a configuration builder to use as the base for building either the local or Azure
            // configuration.
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables();

            // Build the configuration and make it available for dependency injection.
            // NOTE: We do not register this configuration globally as an IConfiguration. Doing so would result in the
            // host.json global config being ignored.
            return configurationBuilder.Build();
        }
    }
}
