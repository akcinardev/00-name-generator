using System.Text.Json.Serialization;

namespace NameGenerator.Models
{
    public class ApiResponse
    {
		[JsonPropertyName("results")]
		public List<Result> Results { get; set; }
    }
}
