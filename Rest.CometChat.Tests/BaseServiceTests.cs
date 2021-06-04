using Moq;
using NUnit.Framework;
using Rest.CometChat.Abstractions;

namespace Rest.CometChat.Tests
{
	public abstract class BaseServiceTests<TService>
	{
		protected abstract TService GetService();

		protected TService? Service { get; private set; }

		protected Mock<ICometChatConfig>? CometChatConfigMock { get; private set; }

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			this.CometChatConfigMock = new();
			this.CometChatConfigMock.Setup(x => x.Region).Returns("eu");
			this.CometChatConfigMock.Setup(x => x.AppId).Returns("3445229bc0ae570");
			this.CometChatConfigMock.Setup(x => x.ApiKey).Returns("a47a6899402ad7fff8a2f0274845f641cc809b42");
		}

		[SetUp]
		public void SetUp()
		{
			this.Service = GetService();
		}
	}
}