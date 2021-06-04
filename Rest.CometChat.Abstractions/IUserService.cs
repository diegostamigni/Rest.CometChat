using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rest.CometChat.Requests;
using Rest.CometChat.Responses;
using Rest.CometChat.ServiceModel;

namespace Rest.CometChat.Abstractions
{
	public interface IUserService
	{
		Task<User?> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default);

		Task<PaginatedList<User>?> ListAsync(ListUserOptions options, CancellationToken cancellationToken = default);

		Task<User?> GetAsync(string uid, CancellationToken cancellationToken = default);

		Task<User?> UpdateAsync(UpdateUserRequest request, CancellationToken cancellationToken = default);

		Task<DeactivateUserResponse> DeactivateUserAsync(string uid, CancellationToken token = default);

		Task<DeactivateUsersResponse> DeactivateUsersAsync(List<string> uids, CancellationToken token = default);

		Task<ReactivateUserResponse> ReactivateUsersAsync(List<string> uids, CancellationToken token = default);

		Task<BlockUserResponse> BlockUsersAsync(List<string> uids, CancellationToken token = default);

		Task<UnblockUserResponse> UnblockUsersAsync(List<string> uids, CancellationToken token = default);

		Task<PaginatedList<User>?> ListBlockedUsersAsync(
			ListBlockedUsersOptions options = default,
			CancellationToken token = default);
	}
}