using System.Text.Json;
using NameGenerator.Models;

namespace NameGenerator.Controllers
{
    public class ApiController
	{
		public ApiController() { }

        /// <summary>
        /// Sends GET request to API and and gets the response body.
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <returns>The response body in JSON format.</returns>
        public string GetApiResponseBody(string apiUrl, int queryAmount = 1)
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
		public List<Name>? GetNamesFromJson(string json)
		{
			List<Name>? nameList = new List<Name>();

			ApiResponse? apiResponse = JsonSerializer.Deserialize<ApiResponse>(json);

            foreach(Result result in apiResponse.Results)
            {
                nameList.Add(result.Name);
            }

            return nameList;
        }
	}
}
