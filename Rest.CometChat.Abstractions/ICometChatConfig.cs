using Rest.CometChat.ServiceModel;

namespace Rest.CometChat.Abstractions
{
	public interface ICometChatConfig
	{
		public string? CometChatAppId { get; set; }

		public string? CometChatApiKey { get; set; }

		/// <summary>
		/// The region on which your CometChat application resides. Typically either `us` or `eu`.
		/// </summary>
		public string? CometChatRegion { get; set; }

		public ApiVersion? CometChatApiVersion { get; set; }
	}
}