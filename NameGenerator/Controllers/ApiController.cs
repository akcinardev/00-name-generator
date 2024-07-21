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
		public async Task<List<Name>?> GetRandomNames(int amount = 1)
		{
			// string? jsonResponse = GetApiResponseBody(this.ApiUrl, amount);
			string? jsonResponse = await GetApiResponseBodyAsync(this.ApiUrl, amount);
			List<Name>? randomNames = GetNamesFromJson(jsonResponse);
			if (randomNames == null || randomNames.Count == 0)
			{
				Console.WriteLine("There was a problem while getting the random names.");
			}
			return randomNames;
		}

		/// <summary>
		/// Sends GET request to API and and gets the response body asynchronously.
		/// </summary>
		/// <param name="apiUrl"></param>
		/// <param name="queryAmount"></param>
		/// <returns>The response body in JSON format.</returns>
		private async Task<string> GetApiResponseBodyAsync(string apiUrl, int queryAmount = 1)
		{
			queryAmount = Math.Clamp(queryAmount, 1, 10);

			string apiquery = apiUrl + "?results=" + queryAmount.ToString();

			using (HttpClient httpClient = new HttpClient())
			{
				HttpResponseMessage response = await httpClient.GetAsync(apiquery);
				response.EnsureSuccessStatusCode();

				string responseBody = await response.Content.ReadAsStringAsync();

				return responseBody;
			}
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
