using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Rest.CometChat.Abstractions;
using Shouldly;

namespace Rest.CometChat.Tests
{
	[TestFixture]
	public class FriendServiceTests : BaseServiceTests<IFriendService>
	{
		[Explicit]
		[TestCase("cd2ee81e-e285-4cae-849f-d968dc47785f", "6d5e5a79-38dc-48ea-afb3-88e09a7f31f4")]
		public async Task AddFriend_Success(string uid, params string[] friends)
		{
			var result = await this.Service!.AddAsync(uid, friends.ToList());

			result.ShouldNotBeNull();
			result.Accepted.ShouldNotBeNull();
			result.Accepted!.ShouldContainKey(friends[0]);

			var friendResponse = result.Accepted[friends[0]];
			friendResponse.Success.ShouldBe(true );
		}

		[TestCase("cd2ee81e-e285-4cae-849f-d968dc47785f", "6d5e5a79-38dc-48ea-afb3-88e09a7f31f4")]
		public async Task ListFriend_Success(string uid, string friendUid)
		{
			var result = await this.Service!.ListAsync(uid);
			result.ShouldNotBeNull();
			result.ShouldSatisfyAllConditions
			(
				() => result.Meta.ShouldNotBeNull(),
				() => result.Entities.ShouldNotBeNull(),
				() => result.Entities!.ShouldContain(x => x.Uid == friendUid)
			);
		}

		[Explicit]
		[TestCase("cd2ee81e-e285-4cae-849f-d968dc47785f", "6d5e5a79-38dc-48ea-afb3-88e09a7f31f4")]
		public async Task RemoveFriend_Success(string uid, params string[] friends)
		{
			var result = await this.Service!.RemoveAsync(uid, friends.ToList());
			result.ShouldNotBeNull();
			result.ShouldSatisfyAllConditions
			(
				() => result.Success.ShouldBe(true)
			);
		}

		protected override IFriendService GetService()
			=> new FriendService(this.CometChatConfigMock!.Object);
	}
}