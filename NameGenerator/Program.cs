using Microsoft.Extensions.Configuration;
using NameGenerator.Controllers;
using NameGenerator.Models;

namespace NameGenerator
{
    internal class Program
	{
		static void Main(string[] args)
		{
			// Load configuration file
			IConfiguration config = LoadConfiguration();
			ApiController apiController = new ApiController(config);

			try
			{
				// Get JSON Response from API
				Task.Run(async () =>
				{
					List<Name>? randomNames = await apiController.GetRandomNames(11);

					if (randomNames?.Count > 0)
					{
						DisplayNames(randomNames);
					}

				}).GetAwaiter().GetResult();
				

				
			}
			catch (Exception ex)
			{
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

		private static IConfiguration LoadConfiguration()
		{
			ConfigController configController = new ConfigController();
			return configController.InitiateConfig();
		}

		private static void DisplayNames(List<Name> randomNames)
		{
			foreach (Name name in randomNames)
			{
				Console.WriteLine($"Random Name: {name.First} {name.Last}");
			}
		}

	}
}
