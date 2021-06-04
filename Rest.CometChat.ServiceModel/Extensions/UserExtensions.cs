using System;

namespace Rest.CometChat.ServiceModel.Extensions
{
	public static class UserExtensions
	{
		public static DateTime? CreatedAtDateTime(this User user)
		{
			if (user.CreatedAt is null)
			{
				return null;
			}

			var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(user.CreatedAt.Value);
			return dateTimeOffset.UtcDateTime;
		}

		public static DateTime? UpdatedAtDateTime(this User user)
		{
			if (user.UpdatedAt is null)
			{
				return null;
			}

			var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(user.UpdatedAt.Value);
			return dateTimeOffset.UtcDateTime;
		}
	}
}