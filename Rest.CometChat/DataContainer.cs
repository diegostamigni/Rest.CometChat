using System.Text.Json.Serialization;

namespace Rest.CometChat
{
	internal class DataContainer<TEntity>
	{
		[JsonPropertyName("data")]
		public TEntity? Entity { get; set; }
	}
}