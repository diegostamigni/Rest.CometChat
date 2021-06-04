using System;
using System.Net.Http;
using System.Text.Json;
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

		protected JsonSerializerOptions JsonSerializerOptions = new()
		{
			PropertyNameCaseInsensitive = true,
			IgnoreNullValues = true
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
	}
}