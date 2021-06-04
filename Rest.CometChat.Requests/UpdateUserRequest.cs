using System.Collections.Generic;

namespace Rest.CometChat.Requests
{
	public record UpdateUserRequest(string Uid)
	{
		/// <summary>
		/// Display name of the user.
		/// </summary>
		public string? Name { get; set; }

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
		/// If you plan to use Email Notification or SMS Notification extensions, Please add the private metadata here.
		/// </summary>
		public object? Metadata { get; set; }

		/// <summary>
		/// Updates tags of a specific group.
		/// </summary>
		public List<string>? Tags { get; set; }
	}
}