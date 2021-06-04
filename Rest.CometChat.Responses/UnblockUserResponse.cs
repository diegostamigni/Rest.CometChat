using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rest.CometChat.Responses
{
	public class UnblockUserResponse
	{
		[JsonPropertyName("data")]
		public Dictionary<string, BaseResponse>? UnblockedUser { get; set; }
	}
}