using System.Collections.Generic;

namespace Rest.CometChat.Responses
{
	public class ReactivateUserResponse : BaseResponse
	{
		public List<string>? ReactivatedUids { get; set; }
	}
}