using Microsoft.Extensions.Configuration;
using NameGenerator.Controllers;

namespace NameGenerator
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ApiController apiController = new ApiController();
			IConfiguration config;
			string jsonResponse;
			List<Name> randomNames;

			// Initialize config
			var builder = new ConfigurationBuilder()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			config = builder.Build();

			// Get JSON Response from API
			jsonResponse = apiController.GetApiResponseBody(config["ApiURL"], 3);

			// Parse JSON Response and get random name
			randomNames = apiController.GetNamesFromJson(jsonResponse);

			foreach (Name name in randomNames)
			{
				Console.WriteLine($"Random Name: {name.first} {name.last}");
			}
        }
	}
}
