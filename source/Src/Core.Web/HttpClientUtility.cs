using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Core.Web
{
    public class HttpClientUtility : AbstractHttpUtility
    {
        #region Constructor

        public HttpClientUtility(string baseAddress) : base(baseAddress)
        {
        }

        public HttpClientUtility(string baseAddress, IDictionary<String, String> defaultHeaders) : base(baseAddress, defaultHeaders)
        {

        }

        #endregion

        #region Sync

        #region Post

        public override TResponseModel Post<TResponseModel, TErrorResult>(string path, object model, AuthorizationToken token)
        {
            return (TResponseModel)Post<TErrorResult>(typeof(TResponseModel), path, model, token);
        }

        public override object Post<TErrorResult>(Type responseModelType, string path, object model, AuthorizationToken token)
        {
            try
            {
                return Send<TErrorResult>(responseModelType, path, HttpMethod.Post, GetContent(model), token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Get

        public override TResponseModel Get<TResponseModel, TErrorResult>(string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token)
        {
            return (TResponseModel)Get<TErrorResult>(typeof(TResponseModel), path, parameters, token);
        }

        public override object Get<TErrorResult>(Type responseModelType, string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token)
        {
            return Send<TErrorResult>(responseModelType, AppendQueryString(path, parameters), HttpMethod.Get, null, token);
        }

        #endregion

        #endregion

#if !NET40
        #region Async

        #region Post

        public override async Task<TResponseModel> PostAsync<TResponseModel, TErrorResult>(string path, object model, AuthorizationToken token)
        {
            return (TResponseModel)await PostAsync<TErrorResult>(typeof(TResponseModel), path, model, token);
        }

        public override async Task<object> PostAsync<TErrorResult>(Type responseModelType, string path, object model, AuthorizationToken token)
        {
            try
            {
                return await SendAsync<TErrorResult>(responseModelType, path, HttpMethod.Post, GetContent(model), token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Get

        public override async Task<TResponseModel> GetAsync<TResponseModel, TErrorResult>(string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token)
        {
            return (TResponseModel)await GetAsync<TErrorResult>(typeof(TResponseModel), path, parameters, token);
        }

        public override async Task<object> GetAsync<TErrorResult>(Type responseModelType, string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token)
        {
            return await SendAsync<TErrorResult>(responseModelType, AppendQueryString(path, parameters), HttpMethod.Get, null, token);
        }

        #endregion

        #endregion
#endif

        #region Helpers

        private HttpContent GetContent(object model)
        {
            HttpContent content;

            if (model is HttpContent)
            {
                content = model as HttpContent;
            }
            else if (model is IEnumerable<KeyValuePair<String, String>>)
            {
                content = GetUrlEncodedRequestContent(model as IEnumerable<KeyValuePair<String, String>>);
            }
            else
            {
                content = GetStringContent(model);
            }

            return content;
        }

        private StringContent GetStringContent(object model)
        {
            try
            {
                StringContent content = null;

                if (model != null)
                {
                    content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, ContentType);
                }

                return content;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private FormUrlEncodedContent GetUrlEncodedRequestContent(IEnumerable<KeyValuePair<String, String>> parameters)
        {
            try
            {
                return new FormUrlEncodedContent(parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

#if NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
        private TResponseModel Send<TResponseModel, TErrorResult>(string path, HttpMethod method, HttpContent content, AuthorizationToken token) where TErrorResult : ErrorResult
        {
            return (TResponseModel)Send<TErrorResult>(typeof(TResponseModel), path, method,  content, token);
        }

        private object Send<TErrorResult>(Type responseModelType, string path, HttpMethod method,  HttpContent content, AuthorizationToken token) where TErrorResult : ErrorResult
        {
            using (var handler = CreateBypassSslErrorHttpClientHandler())
            {
                using (var client = CreateHttpClient(handler))
                {
                    return Send<TErrorResult>(responseModelType, path, method,  content, token, client);
                }
            }
        }

        private async Task<TResponseModel> SendAsync<TResponseModel, TErrorResult>(string path, HttpMethod method, HttpContent content, AuthorizationToken token)
        {
            return (TResponseModel)await SendAsync<TErrorResult>(typeof(TResponseModel), path, method,  content, token);
        }

        private async Task<object> SendAsync<TErrorResult>(Type responseModelType, string path, HttpMethod method,  HttpContent content, AuthorizationToken token)
        {
            using (var handler = CreateBypassSslErrorHttpClientHandler())
            {
                using (var client = CreateHttpClient(handler))
                {
                    return await SendAsync<TErrorResult>(responseModelType, path, method,  content, token, client);
                }
            }
        }

        private HttpClientHandler CreateBypassSslErrorHttpClientHandler()
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
        }

        private HttpClient CreateHttpClient(HttpClientHandler handler)
        {
            var client = new HttpClient(handler);
            SetHttpClientHeaders(client);

            return client;
        }
#else
        private TResponseModel Send<TResponseModel, TErrorResult>(string path, HttpMethod method, HttpContent content, AuthorizationToken token) where TErrorResult : ErrorResult
        {
            return (TResponseModel)Send<TErrorResult>(typeof(TResponseModel), path, method, content, token);
        }

        private object Send<TErrorResult>(Type responseModelType, string path, HttpMethod method, HttpContent content, AuthorizationToken token) where TErrorResult : ErrorResult
        {
            using (var client = CreateHttpClient())
            {
                return Send<TErrorResult>(responseModelType, path, method, content, token, client);
            }
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            SetHttpClientHeaders(client);

            return client;
        }
#endif

#if NET45 || NET46 || NET47 || NET471 || NET472
        private async Task<TResponseModel> SendAsync<TResponseModel, TErrorResult>(string path, HttpMethod method, HttpContent content, AuthorizationToken token)
        {
            return (TResponseModel)await SendAsync<TErrorResult>(typeof(TResponseModel), path, method, content, token);
        }

        private async Task<object> SendAsync<TErrorResult>(Type responseModelType, string path, HttpMethod method, HttpContent content, AuthorizationToken token)
        {
            using (var client = CreateHttpClient())
            {
                return await SendAsync<TErrorResult>(responseModelType, path, method, content, token, client);
            }
        }
#endif

        private TResponseModel Send<TResponseModel, TErrorResult>(string path, HttpMethod method, HttpContent content, AuthorizationToken token, HttpClient client) where TErrorResult : ErrorResult
        {
            return (TResponseModel)Send<TErrorResult>(typeof(TResponseModel), path, method, content, token, client);
        }

        private object Send<TErrorResult>(Type responseModelType, string path, HttpMethod method, HttpContent content, AuthorizationToken token, HttpClient client) where TErrorResult : ErrorResult
        {
            HttpRequestMessage request = new HttpRequestMessage(method, path);

            if (content != null)
            {
                request.Content = content;
            }

            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(token.TokenType.ToString(), token.Token);
            }

            var result = client.SendAsync(request).Result;
            String json = result.Content.ReadAsStringAsync().Result;

            json = FixJson(json);

            if (result.IsSuccessStatusCode)
            {
                if (responseModelType == typeof(String))
                {
                    return json;
                }
                else
                {
                    return JsonConvert.DeserializeObject(json, responseModelType);
                }
            }
            else
            {
                throw HandleException<TErrorResult>(result.StatusCode, json);
            }
        }

#if !NET40

        private async Task<TResponseModel> SendAsync<TResponseModel, TErrorResult>(string path, HttpMethod method, HttpContent content, AuthorizationToken token, HttpClient client)
        {
            return (TResponseModel)await SendAsync<TErrorResult>(typeof(TResponseModel), path, method, content, token, client);
        }

        private async Task<object> SendAsync<TErrorResult>(Type responseModelType, string path, HttpMethod method, HttpContent content, AuthorizationToken token, HttpClient client)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, path);

            if (content != null)
            {
                request.Content = content;
            }

            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(token.TokenType.ToString(), token.Token);
            }

            var result = await client.SendAsync(request);
            String json = await result.Content.ReadAsStringAsync();

            json = FixJson(json);

            if (result.IsSuccessStatusCode)
            {
                if (responseModelType == typeof(String))
                {
                    return json;
                }
                else
                {
                    return JsonConvert.DeserializeObject(json, responseModelType);
                }
            }
            else
            {
                if (result.StatusCode != HttpStatusCode.Unauthorized)
                {
                    throw new HttpException<TErrorResult>("Unable to perform Http action.", json);
                }
                else
                {
                    throw new UnauthorizedHttpException();
                }
            }
        }

#endif

        private void SetHttpClientHeaders(HttpClient client)
        {
            client.BaseAddress = BaseAddress;

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Accept));

            if (DefaultHeaders != null)
            {
                foreach (var headerItem in DefaultHeaders)
                {
                    client.DefaultRequestHeaders.Add(headerItem.Key, headerItem.Value);
                }
            }

            if (Timeout != 0)
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout);
            }
        }

#endregion
    }
}