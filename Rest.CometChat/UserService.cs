using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
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

		public async Task<User?> CreateAsync(
			CreateUserRequest request,
			CancellationToken cancellationToken = default)
		{
			var requestUri = new Uri(this.BaseUri, "users");

			using var httpRequestMessage = CreateRequest(request, HttpMethod.Post, requestUri);
			using var httpClient = this.HttpClient;
			using var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			var result = await JsonSerializer
				.DeserializeAsync<DataContainer<User>>(stream, this.JsonSerializerOptions, cancellationToken);

			return result?.Entity;
		}

		public async Task<PaginatedList<User>?> ListAsync(
			ListUserOptions? options = default,
			CancellationToken cancellationToken = default)
		{
			var requestUri = new Uri(this.BaseUri, "users");
			var requestUrl = requestUri.AbsoluteUri;
			if (options is not null)
			{
				requestUrl = OptionsToUrlQuery(options, requestUrl);
			}

			using var httpClient = this.HttpClient;
			using var stream = await httpClient.GetStreamAsync(requestUrl);

			return await JsonSerializer
				.DeserializeAsync<PaginatedList<User>>(stream, this.JsonSerializerOptions, cancellationToken);
		}

		public async Task<User?> GetAsync(
			string uid,
			CancellationToken cancellationToken = default)
		{
			using var httpClient = this.HttpClient;
			using var stream = await httpClient.GetStreamAsync(new Uri(this.BaseUri, $"users/{uid}"));

			var result = await JsonSerializer
				.DeserializeAsync<DataContainer<User>>(stream, this.JsonSerializerOptions, cancellationToken);

			return result?.Entity;
		}

		public async Task<User?> UpdateAsync(
			UpdateUserRequest request,
			CancellationToken cancellationToken = default)
		{
			using var httpRequestMessage = CreateRequest(request, HttpMethod.Post, new Uri(this.BaseUri, $"users/{request.Uid}"));
			using var httpClient = this.HttpClient;
			using var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			var result = await JsonSerializer
				.DeserializeAsync<DataContainer<User>>(stream, this.JsonSerializerOptions, cancellationToken);

			return result?.Entity;
		}

		public async Task<DeactivateUserResponse?> DeactivateUserAsync(
			string uid,
			bool permanent,
			CancellationToken cancellationToken = default)
		{
			using var httpClient = this.HttpClient;
			using var httpRequestMessage = CreateRequest(new Dictionary<string, object>()
			{
				{ "permanent", permanent }
			}, HttpMethod.Delete, new Uri(this.BaseUri, $"users/{uid}"));

			using var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			var result = await JsonSerializer
				.DeserializeAsync<DataContainer<DeactivateUserResponse>>(stream, this.JsonSerializerOptions, cancellationToken);

			return result?.Entity;
		}

		public async Task<DeactivateUsersResponse?> DeactivateUsersAsync(
			List<string> uids,
			CancellationToken cancellationToken = default)
		{
			using var httpRequestMessage = CreateRequest(uids, HttpMethod.Delete, new Uri(this.BaseUri, "users"));
			using var httpClient = this.HttpClient;
			using var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			return await JsonSerializer
				.DeserializeAsync<DeactivateUsersResponse>(stream, this.JsonSerializerOptions, cancellationToken);
		}

		public async Task<ReactivateUserResponse?> ReactivateUsersAsync(
			List<string> uids,
			CancellationToken cancellationToken = default)
		{
			using var httpRequestMessage = CreateRequest(uids, HttpMethod.Put, new Uri(this.BaseUri, "users"));
			using var httpClient = this.HttpClient;
			using var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			return await JsonSerializer
				.DeserializeAsync<ReactivateUserResponse>(stream, this.JsonSerializerOptions, cancellationToken);
		}

		public async Task<BlockUserResponse?> BlockUsersAsync(
			List<string> uids,
			CancellationToken cancellationToken = default)
		{
			using var httpRequestMessage = CreateRequest(uids, HttpMethod.Post, new Uri(this.BaseUri, "users/uid/blockedusers"));
			using var httpClient = this.HttpClient;
			using var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			return await JsonSerializer
				.DeserializeAsync<BlockUserResponse>(stream, this.JsonSerializerOptions, cancellationToken);
		}

		public async Task<UnblockUserResponse?> UnblockUsersAsync(
			List<string> uids,
			CancellationToken cancellationToken = default)
		{
			using var httpRequestMessage = CreateRequest(uids, HttpMethod.Delete, new Uri(this.BaseUri, "users/uid/blockedusers"));
			using var httpClient = this.HttpClient;
			using var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			return await JsonSerializer
				.DeserializeAsync<UnblockUserResponse>(stream, this.JsonSerializerOptions, cancellationToken);
		}

		public async Task<PaginatedList<User>?> ListBlockedUsersAsync(
			ListBlockedUsersOptions? options = default,
			CancellationToken cancellationToken = default)
		{
			var requestUri = new Uri(this.BaseUri, "users/uid/blockedusers");
			var requestUrl = requestUri.AbsoluteUri;
			if (options is not null)
			{
				requestUrl = OptionsToUrlQuery(options, requestUrl);
			}

			using var httpClient = this.HttpClient;
			using var stream = await httpClient.GetStreamAsync(requestUrl);

			return await JsonSerializer
				.DeserializeAsync<PaginatedList<User>>(stream, this.JsonSerializerOptions, cancellationToken);
		}
	}
}