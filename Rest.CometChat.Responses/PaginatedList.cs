using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rest.CometChat.Responses
{
	public class PaginatedList<TEntity>
	{
		[JsonPropertyName("data")]
		public List<TEntity>? Entities { get; set; }

		public Meta? Meta { get; set; }
	}

}