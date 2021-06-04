using System.Text.Json.Serialization;

namespace Rest.CometChat.Responses
{
	public class Pagination
	{
		public long? Total { get; set; }

		public long? Count { get; set; }

		[JsonPropertyName("per_page")]
		public long? PerPage { get; set; }

		[JsonPropertyName("current_page")]
		public long? CurrentPage { get; set; }

		[JsonPropertyName("total_pages")]
		public long? TotalPages { get; set; }
	}
}