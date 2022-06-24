using Microsoft.Extensions.Configuration;

namespace Kvpbldsck.NastolochkiAPI.Common.Contract.Configuration;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddEnvFile(this IConfigurationBuilder builder, string envFilePath)
    {
        return builder.Add(new EnvFileConfigurationSource(envFilePath));
    }
}
