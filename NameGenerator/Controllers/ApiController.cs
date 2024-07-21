using System.Text.Json;
using Microsoft.Extensions.Configuration;
using NameGenerator.Models;

namespace NameGenerator.Controllers
{
    public class ApiController
	{
        private string? ApiUrl { get; set; }
        public List<Name>? RandomNames { get; set; }

		public ApiController(IConfiguration config)
		{
			ApiUrl = GetApiUrl(config);
		}

		/// <summary>
		/// Gets random name list from API. 1 < amount < 10
		/// </summary>
		/// <param name="amount"></param>
		/// <returns></returns>
		public List<Name>? GetRandomNames(int amount = 1)
		{
			string? jsonResponse = GetApiResponseBody(this.ApiUrl, amount);
			List<Name>? randomNames = GetNamesFromJson(jsonResponse);
			if (randomNames == null || randomNames.Count == 0)
			{
				Console.WriteLine("There was a problem while getting the random names.");
			}
			return randomNames;
		}

		/// <summary>
		/// Sends GET request to API and and gets the response body.
		/// </summary>
		/// <param name="apiUrl"></param>
		/// <returns>The response body in JSON format.</returns>
		private string GetApiResponseBody(string apiUrl, int queryAmount = 1)
		{
			queryAmount = queryAmount < 1 ? 1 : queryAmount;
			queryAmount = queryAmount > 10 ? 10 : queryAmount;

			string apiquery = apiUrl + "?results=" + queryAmount.ToString();

            HttpClient client = new HttpClient();

			HttpResponseMessage response = client.GetAsync(apiquery).Result;
			response.EnsureSuccessStatusCode();

			string responseBody = response.Content.ReadAsStringAsync().Result;

            return responseBody;
		}

		/// <summary>
		/// Parses given json string and gets random name object/s.
		/// </summary>
		/// <param name="json"></param>
		/// <returns><see cref="Name"/></returns>
		private List<Name>? GetNamesFromJson(string json)
		{
			List<Name>? nameList = new List<Name>();

			ApiResponse? apiResponse = JsonSerializer.Deserialize<ApiResponse>(json);

            foreach(Result result in apiResponse.Results)
            {
                nameList.Add(result.Name);
            }

            return nameList;
        }

		/// <summary>
		/// Gets API URL from given configuration and validates.
		/// </summary>
		/// <param name="config"></param>
		/// <returns>API URL as string.</returns>
		private static string GetApiUrl(IConfiguration config)
		{
			string? apiUrl = config["ApiURL"];
			if (apiUrl == null)
			{
				throw new Exception("API URL could not be found on config.");
			}
			return apiUrl;
		}
	}
}
