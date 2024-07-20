using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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
		/// Parses given json string and gets a random name object.
		/// </summary>
		/// <param name="json"></param>
		/// <returns><see cref="Name"/></returns>
		public List<Name>? GetNamesFromJson(string json)
		{
			List<Name>? nameList = new List<Name>();

			ApiResponse apiResponse = JsonSerializer.Deserialize<ApiResponse>(json);

            foreach(Result result in apiResponse.results)
            {
                nameList.Add(result.name);
            }

            return nameList;

            // foreach(Result result in apiResponse.results)
            // {
            //     // nameList.Add(result.name);
            //     Console.WriteLine(result.name);
            // }
            // 
            // if (apiResponse != null || apiResponse.results.Count > 0)
            // {
            // 	return nameList;
            // }
            // else
            // {
            // 	return null;
            // }
        }
	}
}
