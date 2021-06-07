using System.Collections.Generic;

namespace Rest.CometChat.Responses
{
	public class DeactivateUsersResponse : BaseResponse
	{
		public List<string>? AlreadyDeactivatedUids { get; set; }

		public List<string>? DeactivatedUids { get; set; }

		public List<string>? NotFound { get; set; }
	}
}