using System.Collections.Generic;

namespace Rest.CometChat.ServiceModel
{
	public class User
	{
		/// <summary>
		/// Unique identifier of the user. Please refer to https://prodocs.cometchat.com/docs/concepts#uid
		/// </summary>
		public string? Uid { get; set; }

		/// <summary>
		/// Display name of the user.
		/// </summary>
		public string? Name { get; set; }

		public string? Status { get; set; }

		/// <summary>
		/// URL to profile picture of the user.
		/// </summary>
		public string? Avatar { get; set; }

		/// <summary>
		/// User role of the user for role based access control.
		/// </summary>
		public string? Role { get; set; }

		public long? CreatedAt { get; set; }

		public long? UpdatedAt { get; set; }

		public bool? HasBlockedMe { get; set; }

		public bool? BlockedByMe { get; set; }

		public Dictionary<string, object>? Metadata { get; set; }
	}
}