using Microsoft.Extensions.Configuration;

namespace Swizzer.Web.Infrastructure.Framework.Extensions
{
    public static class ConfigurationExtensions
    {
        public static TSettings CreateSettings<TSettings>(this IConfiguration configuration)
            where TSettings : new()
        {
            var section = typeof(TSettings).Name.Replace("Settings", string.Empty);
            var configurationValue = new TSettings();
            configuration.GetSection(section).Bind(configurationValue);

            return configurationValue;
        }
    }
}
