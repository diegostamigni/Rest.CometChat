using System.Collections.Generic;

namespace Rest.CometChat.Requests
{
	public record CreateUserRequest(string Uid, string Name)
	{
		/// <summary>
		/// URL to profile picture of the user.
		/// </summary>
		public string? Avatar { get; set; }

		/// <summary>
		/// URL to profile page.
		/// </summary>
		public string? Link { get; set; }

		/// <summary>
		/// User role of the user for role based access control.
		/// </summary>
		public string? Role { get; set; }

		/// <summary>
		/// Additional information about the user as JSON.
		/// </summary>
		public Dictionary<string, object>? Metadata { get; set; }

		/// <summary>
		/// Includes authToken of created user in response.
		/// </summary>
		public bool? WithAuthToken { get; set; }

		/// <summary>
		/// A list of tags to identify specific users.
		/// </summary>
		public List<string>? Tags { get; set; }
	}
}