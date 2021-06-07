using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rest.CometChat.Requests;
using Rest.CometChat.Responses;
using Rest.CometChat.ServiceModel;

namespace Rest.CometChat.Abstractions
{
	public interface IFriendService
	{
		Task<AddFriendResponse?> AddAsync(
			string uid,
			List<string> friendUids,
			CancellationToken cancellationToken = default);

		Task<PaginatedList<User>?> ListAsync(
			string uid,
			ListUserOptions? options = default,
			CancellationToken cancellationToken = default);

		Task<BaseResponse?> RemoveAsync(
			string uid,
			List<string> friendUids,
			CancellationToken cancellationToken = default);
	}
}