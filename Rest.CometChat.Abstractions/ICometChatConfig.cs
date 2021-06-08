namespace Rest.CometChat.Abstractions
{
	public interface ICometChatConfig
	{
		public string? AppId { get; set; }

		public string? ApiKey { get; set; }

		/// <summary>
		/// The region on which your CometChat application resides. Typically either `us` or `eu`.
		/// </summary>
		public string? Region { get; set; }
	}
}