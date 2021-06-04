using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rest.CometChat.Responses
{
	public class BlockUserResponse
	{
		[JsonPropertyName("data")]
		public Dictionary<string, BaseResponse>? BlockedUser { get; set; }
	}
}