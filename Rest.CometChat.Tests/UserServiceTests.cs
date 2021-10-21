using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Rest.CometChat.Abstractions;
using Rest.CometChat.Requests;
using Rest.CometChat.ServiceModel.Extensions;
using Shouldly;

namespace Rest.CometChat.Tests
{
	[Explicit]
	[TestFixture]
	public class UserServiceTests : BaseServiceTests<IUserService>
	{
		[Test]
		public async Task CreateUser_Success()
		{
			var userId = Guid.NewGuid();
			var userName = userId.ToString("N");

			var result = await this.Service!.CreateAsync(new(userId.ToString(), userName)
			{
				Metadata = new()
				{
					{ "Metadata1", "This is a user metadata" }
				}
			});

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
			var result = await this.Service!.ListAsync();
			result.ShouldNotBeNull();
			result.ShouldSatisfyAllConditions
			(
				() => result.Meta.ShouldNotBeNull(),
				() => result.Entities!.ShouldNotBeEmpty(),
				() => result.Entities!.ShouldContain(x => x.Uid == "superhero5")
			);
		}

		[Test]
		public async Task ListUsers_WithOptions_Success()
		{
			var options = new ListUserOptions
			{
				PerPage = 1
			};

			var result = await this.Service!.ListAsync(options);

			result.ShouldNotBeNull();
			result.ShouldSatisfyAllConditions
			(
				() => result.Meta.ShouldNotBeNull(),
				() => result.Entities!.Count.ShouldBe(1)
			);

			options.Page = (result.Meta?.Pagination?.CurrentPage ?? 0) + 1;
			var continuation = await this.Service!.ListAsync(options);

			continuation.ShouldNotBeNull();
			continuation.ShouldSatisfyAllConditions
			(
				() => continuation.Meta.ShouldNotBeNull(),
				() => continuation.Entities!.Count.ShouldBe(1),
				() => continuation.Entities!.ShouldNotContain(x => x.Uid == result.Entities!.Single().Uid)
			);
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

		[TestCase("d05178fe-7741-48c8-b2be-e4e458c4eb53")]
		public async Task UpdateUser_Success(string uid)
		{
			var newName = $"Name {Guid.NewGuid():N}";
			var newAvatar = "https://cdn.britannica.com/59/182859-050-AB2875BA/Christopher-Reeve-Superman.jpg";
			var result = await this.Service!.UpdateAsync(new(uid)
			{
				Name = newName,
				Avatar = newAvatar
			});

			result.ShouldNotBeNull();
			result.ShouldSatisfyAllConditions
			(
				() => result.Uid.ShouldBe(uid),
				() => result.Name.ShouldBe(newName),
				() => result.Avatar.ShouldBe(newAvatar),
				() => result.UpdatedAtDateTime().ShouldNotBeNull()
			);
		}

		[TestCase("d740a4db-e2c9-4fbd-879a-49f378c2690a", true)]
		[TestCase("c5435812-212e-475e-ab1e-c0c154eda3c7", false)]
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

		[TestCase("d05178fe-7741-48c8-b2be-e4e458c4eb53", "7853eb99-e8e1-4ec4-b12b-3c5434d51c02")]
		public async Task DeactivateUsers_Success(params string[] uids)
		{
			var result = await this.Service!.DeactivateUsersAsync(uids.ToList());
			result.ShouldNotBeNull();
			result.ShouldSatisfyAllConditions
			(
				() => result.DeactivatedUids!.All(uids.Contains).ShouldBeTrue()
			);
		}

		[TestCase("d05178fe-7741-48c8-b2be-e4e458c4eb53", "7853eb99-e8e1-4ec4-b12b-3c5434d51c02")]
		public async Task ReactivateUsers_Success(params string[] uids)
		{
			var result = await this.Service!.ReactivateUsersAsync(uids.ToList());
			result.ShouldNotBeNull();
			result.ShouldSatisfyAllConditions
			(
				() => result.ReactivatedUids!.All(uids.Contains).ShouldBeTrue()
			);
		}

		protected override IUserService GetService()
			=> new UserService(this.CometChatConfigMock!.Object);
	}
}