using System.Text.Json.Serialization;

namespace NameGenerator.Models
{
    public class Result
    {
		[JsonPropertyName("gender")]
		public string Gender { get; set; }

		[JsonPropertyName("name")]
		public Name Name { get; set; }
    }
}
