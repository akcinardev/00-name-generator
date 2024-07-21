using Microsoft.Extensions.Configuration;
using NameGenerator.Controllers;
using NameGenerator.Models;

namespace NameGenerator
{
    internal class Program
	{
		static void Main(string[] args)
		{
			ApiController apiController = new ApiController();
			ConfigController configController = new ConfigController();
			IConfiguration config = configController.InitiateConfig();
			List<Name>? randomNames;
			string jsonResponse;
			string? API_URL;


			// Get JSON Response from API
			API_URL = config["ApiURL"];
			if (API_URL != null)
			{
				jsonResponse = apiController.GetApiResponseBody(API_URL, 3);
			}
			else
			{
				throw new Exception("API URL could not be found on config.");
			}
			

			// Parse JSON Response and get random name
			randomNames = apiController.GetNamesFromJson(jsonResponse);

			foreach (Name name in randomNames)
			{
				Console.WriteLine($"Random Name: {name.First} {name.Last}");
			}
        }
	}
}
