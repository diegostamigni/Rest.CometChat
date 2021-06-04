using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
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

		public Task<User?> CreateAsync(
			CreateUserRequest request,
			CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public Task<PaginatedList<User>?> ListAsync(
			ListUserOptions options,
			CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public async Task<User?> GetAsync(
			string uid,
			CancellationToken cancellationToken = default)
		{
			var requestUri = new Uri(this.BaseUri, $"users/{uid}");

			using var httpClient = this.HttpClient;
			using var stream = await httpClient.GetStreamAsync(requestUri);

			var result = await JsonSerializer
				.DeserializeAsync<DataContainer<User>>(stream, this.JsonSerializerOptions, cancellationToken);

			return result?.Entity;
		}

		public Task<User?> UpdateAsync(
			UpdateUserRequest request,
			CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public async Task<DeactivateUserResponse?> DeactivateUserAsync(
			string uid,
			bool permanent,
			CancellationToken cancellationToken = default)
		{
			var requestUri = new Uri(this.BaseUri, $"users/{uid}");

			using var httpClient = this.HttpClient;
			httpClient.DefaultRequestHeaders.Add("permanent", permanent.ToString());

			using var response = await httpClient.DeleteAsync(requestUri, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			return await JsonSerializer
				.DeserializeAsync<DeactivateUserResponse>(stream, this.JsonSerializerOptions, cancellationToken);
		}

		public async Task<DeactivateUsersResponse?> DeactivateUsersAsync(
			List<string> uids,
			CancellationToken cancellationToken = default)
		{
			var requestUri = new Uri(this.BaseUri, $"users");
			using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = new StringContent(JsonSerializer.Serialize(uids), Encoding.UTF8)
			};

			using var httpClient = this.HttpClient;
			using var response = await httpClient.SendAsync(request, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			return await JsonSerializer
				.DeserializeAsync<DeactivateUsersResponse>(stream, this.JsonSerializerOptions, cancellationToken);
		}

		public async Task<ReactivateUserResponse?> ReactivateUsersAsync(
			List<string> uids,
			CancellationToken cancellationToken = default)
		{
			var requestUri = new Uri(this.BaseUri, $"users");
			using var request = new HttpRequestMessage(HttpMethod.Put, requestUri)
			{
				Content = new StringContent(JsonSerializer.Serialize(uids), Encoding.UTF8)
			};

			using var httpClient = this.HttpClient;
			using var response = await httpClient.SendAsync(request, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			return await JsonSerializer
				.DeserializeAsync<ReactivateUserResponse>(stream, this.JsonSerializerOptions, cancellationToken);
		}

		public async Task<BlockUserResponse?> BlockUsersAsync(
			List<string> uids,
			CancellationToken cancellationToken = default)
		{
			var requestUri = new Uri(this.BaseUri, $"users/uid/blockedusers");
			using var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
			{
				Content = new StringContent(JsonSerializer.Serialize(uids), Encoding.UTF8)
			};

			using var httpClient = this.HttpClient;
			using var response = await httpClient.SendAsync(request, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			return await JsonSerializer
				.DeserializeAsync<BlockUserResponse>(stream, this.JsonSerializerOptions, cancellationToken);
		}

		public async Task<UnblockUserResponse?> UnblockUsersAsync(
			List<string> uids,
			CancellationToken cancellationToken = default)
		{
			var requestUri = new Uri(this.BaseUri, $"users/uid/blockedusers");
			using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri)
			{
				Content = new StringContent(JsonSerializer.Serialize(uids), Encoding.UTF8)
			};

			using var httpClient = this.HttpClient;
			using var response = await httpClient.SendAsync(request, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			return await JsonSerializer
				.DeserializeAsync<UnblockUserResponse>(stream, this.JsonSerializerOptions, cancellationToken);
		}

		public Task<PaginatedList<User>?> ListBlockedUsersAsync(
			ListBlockedUsersOptions options = default,
			CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}
}