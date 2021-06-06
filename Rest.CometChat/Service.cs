using System;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Rest.CometChat.Abstractions;

namespace Rest.CometChat
{
	public abstract class Service
	{
		private readonly ICometChatConfig config;
		private readonly IHttpClientFactory? httpClientFactory;
		private readonly HttpClient? httpClient;

		protected string BaseUrl => $"https://api-{this.config.Region}.cometchat.io/v2.0/";

		protected Uri BaseUri => new Uri(this.BaseUrl);

		protected readonly JsonSerializerOptions JsonSerializerOptions = new()
		{
			PropertyNameCaseInsensitive = true,
			IgnoreNullValues = true,
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};

		protected HttpClient HttpClient
		{
			get
			{
				HttpClient configuredHttpClient;
				if (this.httpClient is not null)
				{
					configuredHttpClient = this.httpClient;
				}
				else
				{
					configuredHttpClient = this.httpClientFactory is not null
						? this.httpClientFactory.CreateClient()
						: new();
				}

				if (configuredHttpClient is null)
				{
					throw new ArgumentNullException(nameof(configuredHttpClient), "Invalid http client");
				}

				configuredHttpClient.DefaultRequestHeaders.Add("appId", this.config.AppId);
				configuredHttpClient.DefaultRequestHeaders.Add("apiKey", this.config.ApiKey);
				configuredHttpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
				return configuredHttpClient;
			}
		}

		protected Service(ICometChatConfig config)
		{
			this.config = config;
		}

		protected Service(ICometChatConfig config, HttpClient httpClient)
		{
			this.config = config;
			this.httpClient = httpClient;
		}

		protected Service(ICometChatConfig config, IHttpClientFactory httpClientFactory)
		{
			this.config = config;
			this.httpClientFactory = httpClientFactory;
		}

		protected static string OptionsToUrlQuery<TOptions>(TOptions options, string baseUrl)
		{
			foreach (var propertyInfo in typeof(TOptions).GetProperties(BindingFlags.Public))
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