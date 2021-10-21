using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Rest.CometChat.Abstractions;
using Shouldly;

namespace Rest.CometChat.Tests
{
	[Explicit]
	[TestFixture]
	public class FriendServiceTests : BaseServiceTests<IFriendService>
	{
		[TestCase("d05178fe-7741-48c8-b2be-e4e458c4eb53", "7853eb99-e8e1-4ec4-b12b-3c5434d51c02")]
		public async Task AddFriend_Success(string uid, params string[] friends)
		{
			var result = await this.Service!.AddAsync(uid, friends.ToList());

			result.ShouldNotBeNull();
			result.Accepted.ShouldNotBeNull();
			result.Accepted!.ShouldContainKey(friends[0]);

			var friendResponse = result.Accepted[friends[0]];
			friendResponse.Success.ShouldBe(true );
		}

		[TestCase("d05178fe-7741-48c8-b2be-e4e458c4eb53", "7853eb99-e8e1-4ec4-b12b-3c5434d51c02")]
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

		[TestCase("d05178fe-7741-48c8-b2be-e4e458c4eb53", "7853eb99-e8e1-4ec4-b12b-3c5434d51c02")]
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