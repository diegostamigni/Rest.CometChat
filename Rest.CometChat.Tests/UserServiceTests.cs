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
		[Test]
		public async Task CreateUser_Success()
		{
			var userId = Guid.NewGuid();
			var userName = userId.ToString("N");

			var result = await this.Service!.CreateAsync(new(userId.ToString(), userName));
			result.ShouldNotBeNull();
			result.ShouldSatisfyAllConditions
			(
				() => result.Name.ShouldBe(userName),
				() => result.Uid.ShouldBe(userId.ToString()),
				() => result.CreatedAtDateTime().ShouldNotBeNull()
			);
		}

		[Test]
		public async Task ListUsers_Success()
		{
			throw new NotImplementedException();
		}

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
		public async Task UpdateUser_Success()
		{
			throw new NotImplementedException();
		}

		[TestCase("190cee76-2c25-409e-a0c3-dabe451521e1", true)]
		[TestCase("190cee76-2c25-409e-a0c3-dabe451521e1", false)]
		public async Task DeactivateUser_Success(string uid, bool permanent)
		{
			var result = await this.Service!.DeactivateUserAsync(uid, permanent);
			result.ShouldNotBeNull();

			if (permanent)
			{
				result.ShouldSatisfyAllConditions
				(
					() => result.Message.ShouldNotBeNullOrEmpty(),
					() => result.Success.ShouldBe(true)
				);
			}
			else
			{
				result.DeactivatedUids.ShouldNotBeEmpty();
			}
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

		[Test]
		public async Task ListBlockedUsers_Success()
		{
			throw new NotImplementedException();
		}

		protected override IUserService GetService()
			=> new UserService(this.CometChatConfigMock!.Object);
	}
}