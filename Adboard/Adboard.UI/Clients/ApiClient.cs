﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Adboard.UI.Clients
{
    public abstract class ApiClient
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor _accessor;

        protected ApiClient(HttpClient client, IHttpContextAccessor accessor)
        {
            _client = client;
            _accessor = accessor;
        }

        protected async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            using (var message = new HttpRequestMessage(HttpMethod.Post, url) { Content = CreateContent(request) })
            {
                var token = await _accessor.HttpContext.GetTokenAsync("access_token");
                if (string.IsNullOrWhiteSpace(token) == false)
                    message.Headers.Add("Authorization", $"Bearer {token}");

                using (var response = await _client.SendAsync(message))
                {
                    // Добавить корректный тип исключения и расширить сообщение об ошибке.
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new ApplicationException("Возникла ошибка при выполнении запроса.");

                    var json = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TResponse>(json);
                }
            }
        }

        private static HttpContent CreateContent<TRequest>(TRequest request)
        {
            var json = JsonConvert.SerializeObject(request);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
