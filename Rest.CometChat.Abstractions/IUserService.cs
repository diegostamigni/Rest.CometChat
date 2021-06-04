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
		Task<User?> CreateAsync(
			CreateUserRequest request,
			CancellationToken cancellationToken = default);

		Task<PaginatedList<User>?> ListAsync(
			ListUserOptions? options,
			CancellationToken cancellationToken = default);

		Task<User?> GetAsync(
			string uid,
			CancellationToken cancellationToken = default);

		Task<User?> UpdateAsync(
			UpdateUserRequest request,
			CancellationToken cancellationToken = default);

		Task<DeactivateUserResponse?> DeactivateUserAsync(
			string uid,
			bool permanent,
			CancellationToken cancellationToken = default);

		Task<DeactivateUsersResponse?> DeactivateUsersAsync(
			List<string> uids,
			CancellationToken cancellationToken = default);

		Task<ReactivateUserResponse?> ReactivateUsersAsync(
			List<string> uids,
			CancellationToken cancellationToken = default);

		Task<BlockUserResponse?> BlockUsersAsync(
			List<string> uids,
			CancellationToken cancellationToken = default);

		Task<UnblockUserResponse?> UnblockUsersAsync(
			List<string> uids,
			CancellationToken cancellationToken = default);

		Task<PaginatedList<User>?> ListBlockedUsersAsync(
			ListBlockedUsersOptions? options = default,
			CancellationToken token = default);
	}
}