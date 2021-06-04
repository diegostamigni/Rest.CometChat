using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rest.CometChat.Responses
{
	public class ReactivateUserResponse
	{
		[JsonPropertyName("data")]
		public Dictionary<string, BaseResponse>? ReactivatedUsers { get; set; }
	}
}