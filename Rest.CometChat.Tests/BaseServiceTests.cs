using Moq;
using NUnit.Framework;
using Rest.CometChat.Abstractions;
using Rest.CometChat.ServiceModel;

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
			this.CometChatConfigMock.Setup(x => x.CometChatRegion).Returns("eu");
			this.CometChatConfigMock.Setup(x => x.CometChatAppId).Returns("196099f20fa2804c");
			this.CometChatConfigMock.Setup(x => x.CometChatApiKey).Returns("237425b0dc89339a5c36e10273035c24f42e487c");
			this.CometChatConfigMock.Setup(x => x.CometChatApiVersion).Returns(ApiVersion.V3);
		}

		[SetUp]
		public void SetUp()
		{
			this.Service = GetService();
		}
	}
}