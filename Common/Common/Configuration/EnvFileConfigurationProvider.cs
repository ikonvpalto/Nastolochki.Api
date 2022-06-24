using System.Text.RegularExpressions;
using Kvpbldsck.NastolochkiAPI.Common.Contract.Resources;
using Microsoft.Extensions.Configuration;

namespace Kvpbldsck.NastolochkiAPI.Common.Contract.Configuration;

public sealed class EnvFileConfigurationProvider : ConfigurationProvider
{
    private static readonly Regex EnvFileParseRegex = new (@"\s*(\w+)\s*=\s*(\w+)\s*");

    private readonly string _filePath;

    public EnvFileConfigurationProvider(string filePath)
    {
        _filePath = filePath;
    }

    public override void Load()
    {
        if (!File.Exists(_filePath))
        {
            throw new ArgumentException(string.Format(Localization.EnvFileNotExists, _filePath), nameof(_filePath));
        }

        var lines = File.ReadLines(_filePath).ToList();

        if (!lines.All(EnvFileParseRegex.IsMatch))
        {
            throw new FormatException(string.Format(Localization.FileIsNotEnv, _filePath));
        }

        Data = lines
            .Select(l => EnvFileParseRegex.Match(l))
            .ToDictionary(m => m.Groups[1].Value, m => m.Groups[2].Value);
    }
}
