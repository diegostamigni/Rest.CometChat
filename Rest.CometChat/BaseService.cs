using System;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.WebUtilities;
using Rest.CometChat.Abstractions;
using Rest.CometChat.ServiceModel;

namespace Rest.CometChat
{
	public abstract class BaseService
	{
		private readonly ICometChatConfig config;
		private readonly IHttpClientFactory? httpClientFactory;

		protected string BaseUrl => this.config.CometChatApiVersion switch
		{
			ApiVersion.V2 or null => $"https://api-{this.config.CometChatRegion}.cometchat.io/v2.0/",
			ApiVersion.V3 => $"https://{this.config.CometChatAppId}.api-{this.config.CometChatRegion}.cometchat.io/v3/",
			_ => throw new ArgumentOutOfRangeException($"{this.config.CometChatApiVersion}")
		};

		protected Uri BaseUri => new Uri(this.BaseUrl);

		protected readonly JsonSerializerOptions JsonSerializerOptions = new()
		{
			PropertyNameCaseInsensitive = true,
			DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		protected HttpClient HttpClient
		{
			get
			{
				var configuredHttpClient = this.httpClientFactory is not null
					? this.httpClientFactory.CreateClient()
					: new();

				if (configuredHttpClient is null)
				{
					throw new ArgumentNullException(nameof(configuredHttpClient), "Invalid http client");
				}

				configuredHttpClient.DefaultRequestHeaders.Add("appId", this.config.CometChatAppId);
				configuredHttpClient.DefaultRequestHeaders.Add("apiKey", this.config.CometChatApiKey);
				configuredHttpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
				return configuredHttpClient;
			}
		}

		protected BaseService(ICometChatConfig config)
		{
			this.config = config;
		}

		protected BaseService(ICometChatConfig config, IHttpClientFactory httpClientFactory)
		{
			this.config = config;
			this.httpClientFactory = httpClientFactory;
		}

		protected static string OptionsToUrlQuery<TOptions>(TOptions options, string baseUrl)
		{
			foreach (var propertyInfo in typeof(TOptions).GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				var propertyName = JsonNamingPolicy.CamelCase.ConvertName(propertyInfo.Name);
				var propertyValue = propertyInfo.GetValue(options);
				if (propertyValue is not null)
				{
					baseUrl = QueryHelpers.AddQueryString(baseUrl, propertyName, propertyValue.ToString());
				}
			}

			return baseUrl;
		}

		protected HttpRequestMessage CreateRequest<TRequest>(TRequest request, HttpMethod httpMethod, Uri requestUri)
		{
			var requestJson = JsonSerializer.Serialize(request, this.JsonSerializerOptions);
			return new(httpMethod, requestUri)
			{
				Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
			};
		}

		protected HttpRequestMessage CreateRequest<TRequest>(TRequest request, HttpMethod httpMethod, string requestUrl)
			=> CreateRequest(request, httpMethod, new Uri(requestUrl));
	}
}