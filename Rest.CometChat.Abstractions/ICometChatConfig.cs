namespace Rest.CometChat.Abstractions
{
	public interface ICometChatConfig
	{
		public string AppId { get; set; }

		public string ApiKey { get; set; }

		public string Region { get; set; }
	}
}