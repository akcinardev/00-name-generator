using Microsoft.Extensions.Configuration;

namespace NameGenerator.Controllers
{
	public class ConfigController
	{
        public ConfigController()
        {
        }

        public IConfiguration InitiateConfig()
        {
			var builder = new ConfigurationBuilder()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			IConfiguration config = builder.Build();
			return config;
		}
    }
}
