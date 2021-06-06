using System.Collections.Generic;

namespace Rest.CometChat.Responses
{
	public class DeactivateUserResponse : BaseResponse
	{
		public List<string>? DeactivatedUids { get; set; }
	}
}