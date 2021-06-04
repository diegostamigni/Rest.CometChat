using System;

namespace Rest.CometChat.Responses.Extensions
{
	public static class CursorExtensions
	{
		public static DateTime? UpdatedAtDateTime(this Cursor cursor)
		{
			if (cursor.UpdatedAt is null)
			{
				return null;
			}

			var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(cursor.UpdatedAt.Value);
			return dateTimeOffset.UtcDateTime;
		}
	}
}