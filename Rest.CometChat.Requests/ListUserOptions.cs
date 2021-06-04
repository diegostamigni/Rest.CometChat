using System.Collections.Generic;

namespace Rest.CometChat.Requests
{
	public struct ListUserOptions
	{
		/// <summary>
		/// Searches for given keyword in user's list (either uid, name or email)
		/// </summary>
		public string? SearchKey { get; set; }

		/// <summary>
		/// User list can be fetched depending on the user status. (available, busy, away, offline)
		/// </summary>
		public string? Status { get; set; }

		/// <summary>
		/// User's count will be fetched
		/// </summary>
		public bool? Count { get; set; }

		/// <summary>
		/// Number of users to be fetched in a request. The default value is 100 and the maximum value is 1000.
		/// </summary>
		public int? PerPage { get; set; }

		/// <summary>
		/// Page number.
		/// </summary>
		public int? Page { get; set; }

		/// <summary>
		/// Retrieves users list based on role.
		/// </summary>
		public string? Role { get; set; }

		/// <summary>
		/// Includes tags in the response.
		/// </summary>
		public bool? WithTags { get; set; }

		/// <summary>
		/// Fetches only those users that have these tags.
		/// </summary>
		public List<string>? Tags { get; set; }

		/// <summary>
		/// Fetches users based on multiple roles.
		/// </summary>
		public List<string>? Roles { get; set; }

		/// <summary>
		/// Fetches all the deactivated users of an app.
		/// </summary>
		public bool? OnlyDeactivated { get; set; }

		/// <summary>
		/// Fetches all the users including deactivated users.
		/// </summary>
		public bool? WithDeactivated { get; set; }
	}
}