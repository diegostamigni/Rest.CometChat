using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Rest.CometChat.Abstractions;
using Rest.CometChat.ServiceModel.Extensions;
using Shouldly;

namespace Rest.CometChat.Tests
{
	[TestFixture]
	public class UserServiceTests : BaseServiceTests<IUserService>
	{
		[TestCase("superhero5")]
		[TestCase("superhero4")]
		[TestCase("superhero3")]
		public async Task GetUser_Success(string uid)
		{
			var result = await this.Service!.GetAsync(uid);
			result.ShouldNotBeNull();
			result.ShouldSatisfyAllConditions
			(
				() => result.Avatar.ShouldNotBeNullOrEmpty(),
				() => result.CreatedAtDateTime().ShouldNotBeNull(),
				() => result.Name.ShouldNotBeNullOrEmpty()
			);
		}

		[Test]
		public async Task DeactivateUser_Success()
		{
			throw new NotImplementedException();
		}

		[Test]
		public async Task DeactivateUsers_Success()
		{
			throw new NotImplementedException();
		}

		[Test]
		public async Task ReactivateUsers_Success()
		{
			throw new NotImplementedException();
		}

		[Test]
		public async Task BlockUsers_Success()
		{
			throw new NotImplementedException();
		}

		[Test]
		public async Task UnblockUsers_Success()
		{
			throw new NotImplementedException();
		}

		protected override IUserService GetService()
			=> new UserService(this.CometChatConfigMock!.Object);
	}
}