using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Rest.CometChat.Abstractions;
using Rest.CometChat.Requests;
using Rest.CometChat.Responses;
using Rest.CometChat.ServiceModel;

namespace Rest.CometChat
{

	public class UserService : Service, IUserService
	{
		public UserService(ICometChatConfig config) : base(config)
		{
		}

		public UserService(ICometChatConfig config, HttpClient httpClient)
			: base(config, httpClient)
		{
		}

		public UserService(ICometChatConfig config, IHttpClientFactory httpClientFactory)
			: base(config, httpClientFactory)
		{
		}

		public Task<User?> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<PaginatedList<User>?> ListAsync(ListUserOptions options, CancellationToken cancellationToken = default)
		{
			throw new System.NotImplementedException();
		}

		public async Task<User?> GetAsync(string uid, CancellationToken cancellationToken = default)
		{
			var requestUri = new Uri(this.BaseUri, $"users/{uid}");

			using var httpClient = this.HttpClient;
			using var stream = await httpClient.GetStreamAsync(requestUri);

			var result = await JsonSerializer
				.DeserializeAsync<DataContainer<User>>(stream, this.JsonSerializerOptions, cancellationToken);

			return result?.Entity;
		}

		public Task<User?> UpdateAsync(UpdateUserRequest request, CancellationToken cancellationToken = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<DeactivateUserResponse> DeactivateUserAsync(string uid, CancellationToken token = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<DeactivateUsersResponse> DeactivateUsersAsync(List<string> uids, CancellationToken token = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<ReactivateUserResponse> ReactivateUsersAsync(List<string> uids, CancellationToken token = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<BlockUserResponse> BlockUsersAsync(List<string> uids, CancellationToken token = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<UnblockUserResponse> UnblockUsersAsync(List<string> uids, CancellationToken token = default)
		{
			throw new System.NotImplementedException();
		}

		public Task<PaginatedList<User>?> ListBlockedUsersAsync(ListBlockedUsersOptions options = default, CancellationToken token = default)
		{
			throw new System.NotImplementedException();
		}
	}
}