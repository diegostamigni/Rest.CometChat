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
	public class FriendService : BaseService, IFriendService
	{
		public FriendService(ICometChatConfig config)
			: base(config)
		{
		}

		public FriendService(ICometChatConfig config, HttpClient httpClient)
			: base(config, httpClient)
		{
		}

		public FriendService(ICometChatConfig config, IHttpClientFactory httpClientFactory)
			: base(config, httpClientFactory)
		{
		}

		public async Task<AddFriendResponse?> AddAsync(
			string uid,
			List<string> friendUids,
			CancellationToken cancellationToken = default)
		{
			using var httpClient = this.HttpClient;
			using var httpRequestMessage = CreateRequest(new Dictionary<string, object>
			{
				{ "accepted", friendUids }
			}, HttpMethod.Post, new Uri(this.BaseUri, $"users/{uid}/friends"));

			using var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			var result = await JsonSerializer
				.DeserializeAsync<DataContainer<AddFriendResponse>>(stream, this.JsonSerializerOptions, cancellationToken);

			return result?.Entity;
		}

		public async Task<PaginatedList<User>?> ListAsync(
			string uid,
			ListUserOptions? options = default,
			CancellationToken cancellationToken = default)
		{
			var requestUri = new Uri(this.BaseUri, $"users/{uid}/friends");
			var requestUrl = requestUri.AbsoluteUri;
			if (options is not null)
			{
				requestUrl = OptionsToUrlQuery(options.Value, requestUrl);
			}

			using var httpClient = this.HttpClient;
			using var stream = await httpClient.GetStreamAsync(requestUrl);

			return await JsonSerializer
				.DeserializeAsync<PaginatedList<User>>(stream, this.JsonSerializerOptions, cancellationToken);
		}

		public async Task<BaseResponse?> RemoveAsync(
			string uid,
			List<string> friendUids,
			CancellationToken cancellationToken = default)
		{
			using var httpClient = this.HttpClient;
			using var httpRequestMessage = CreateRequest(new Dictionary<string, object>
			{
				{ "friends", friendUids }
			}, HttpMethod.Delete, new Uri(this.BaseUri, $"users/{uid}/friends"));

			using var response = await httpClient.SendAsync(httpRequestMessage, cancellationToken);
			using var stream = await response.Content.ReadAsStreamAsync();

			var result = await JsonSerializer
				.DeserializeAsync<DataContainer<BaseResponse>>(stream, this.JsonSerializerOptions, cancellationToken);

			return result?.Entity;
		}
	}
}