using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DotFramework.Core.Web
{
    public abstract partial class AbstractHttpUtility : IDisposable
    {
        #region Constructor

        public AbstractHttpUtility(string baseAddress)
        {
            BaseAddress = new Uri(baseAddress);
            Accept = "application/json";
            ContentType = "application/json";
        }

        public AbstractHttpUtility(string baseAddress, IDictionary<String, String> defaultHeaders) : this(baseAddress)
        {
            DefaultHeaders = defaultHeaders;
        }

        #endregion

        #region Properties

        public Uri BaseAddress { get; private set; }

        public IDictionary<String, String> DefaultHeaders { get; private set; }

        /// <summary>
        /// Gets or sets the time-out value in milliseconds
        /// </summary>
        public int Timeout { get; set; }

        public string Accept { get; set; }

        public string ContentType { get; set; }

        #endregion

        #region Sync

        #region Post

        public TResponseModel Post<TResponseModel>(string path)
        {
            return (TResponseModel)Post(typeof(TResponseModel), path);
        }

        public object Post(Type responseModelType, string path)
        {
            return Post<DefaultErrorResult>(responseModelType, path, null, null);
        }

        public TResponseModel Post<TResponseModel>(string path, object model)
        {
            return (TResponseModel)Post(typeof(TResponseModel), path, model);
        }

        public object Post(Type responseModelType, string path, object model)
        {
            return Post<DefaultErrorResult>(responseModelType, path, model, null);
        }

        public TResponseModel Post<TResponseModel>(string path, AuthorizationToken token)
        {
            return (TResponseModel)Post(typeof(TResponseModel), path, null, token);
        }

        public object Post(Type responseModelType, string path, AuthorizationToken token)
        {
            return Post<DefaultErrorResult>(responseModelType, path, null, token);
        }

        public TResponseModel Post<TResponseModel>(string path, object model, AuthorizationToken token)
        {
            return (TResponseModel)Post(typeof(TResponseModel), path, model, token);
        }

        public object Post(Type responseModelType, string path, object model, AuthorizationToken token)
        {
            return Post<DefaultErrorResult>(responseModelType, path, model, token);
        }

        public TResponseModel Post<TResponseModel, TErrorResult>(string path) where TErrorResult : ErrorResult
        {
            return (TResponseModel)Post<TErrorResult>(typeof(TResponseModel), path);
        }

        public object Post<TErrorResult>(Type responseModelType, string path) where TErrorResult : ErrorResult
        {
            return Post<TErrorResult>(responseModelType, path, null, null);
        }

        public TResponseModel Post<TResponseModel, TErrorResult>(string path, object model) where TErrorResult : ErrorResult
        {
            return (TResponseModel)Post<TErrorResult>(typeof(TResponseModel), path, model);
        }

        public object Post<TErrorResult>(Type responseModelType, string path, object model) where TErrorResult : ErrorResult
        {
            return Post<TErrorResult>(responseModelType, path, model, null);
        }

        public TResponseModel Post<TResponseModel, TErrorResult>(string path, AuthorizationToken token) where TErrorResult : ErrorResult
        {
            return (TResponseModel)Post<TErrorResult>(typeof(TResponseModel), path, null, token);
        }

        public object Post<TErrorResult>(Type responseModelType, string path, AuthorizationToken token) where TErrorResult : ErrorResult
        {
            return Post<TErrorResult>(responseModelType, path, null, token);
        }

        public abstract TResponseModel Post<TResponseModel, TErrorResult>(string path, object model, AuthorizationToken token) where TErrorResult : ErrorResult;

        public abstract object Post<TErrorResult>(Type responseModelType, string path, object model, AuthorizationToken token) where TErrorResult : ErrorResult;

        #endregion

        #region Get

        public TResponseModel Get<TResponseModel>(string path)
        {
            return (TResponseModel)Get(typeof(TResponseModel), path);
        }

        public object Get(Type responseModelType, string path)
        {
            return Get<DefaultErrorResult>(responseModelType, path, null, null);
        }

        public TResponseModel Get<TResponseModel>(string path, IEnumerable<KeyValuePair<String, String>> parameters)
        {
            return (TResponseModel)Get(typeof(TResponseModel), path, parameters);
        }

        public object Get(Type responseModelType, string path, IEnumerable<KeyValuePair<String, String>> parameters)
        {
            return Get<DefaultErrorResult>(responseModelType, path, parameters, null);
        }

        public TResponseModel Get<TResponseModel>(string path, AuthorizationToken token)
        {
            return (TResponseModel)Get(typeof(TResponseModel), path, null, token);
        }

        public object Get(Type responseModelType, string path, AuthorizationToken token)
        {
            return Get<DefaultErrorResult>(responseModelType, path, null, token);
        }

        public TResponseModel Get<TResponseModel>(string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token)
        {
            return (TResponseModel)Get(typeof(TResponseModel), path, parameters, token);
        }

        public object Get(Type responseModelType, string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token)
        {
            return Get<DefaultErrorResult>(responseModelType, path, parameters, token);
        }

        public TResponseModel Get<TResponseModel, TErrorResult>(string path) where TErrorResult : ErrorResult
        {
            return (TResponseModel)Get<TErrorResult>(typeof(TResponseModel), path);
        }

        public object Get<TErrorResult>(Type responseModelType, string path) where TErrorResult : ErrorResult
        {
            return Get<TErrorResult>(responseModelType, path, null, null);
        }

        public TResponseModel Get<TResponseModel, TErrorResult>(string path, IEnumerable<KeyValuePair<String, String>> parameters) where TErrorResult : ErrorResult
        {
            return (TResponseModel)Get<TErrorResult>(typeof(TResponseModel), path, parameters);
        }

        public object Get<TErrorResult>(Type responseModelType, string path, IEnumerable<KeyValuePair<String, String>> parameters) where TErrorResult : ErrorResult
        {
            return Get<TErrorResult>(responseModelType, path, parameters, null);
        }

        public TResponseModel Get<TResponseModel, TErrorResult>(string path, AuthorizationToken token) where TErrorResult : ErrorResult
        {
            return (TResponseModel)Get<TErrorResult>(typeof(TResponseModel), path, null, token);
        }

        public object Get<TErrorResult>(Type responseModelType, string path, AuthorizationToken token) where TErrorResult : ErrorResult
        {
            return Get<TErrorResult>(responseModelType, path, null, token);
        }

        public abstract TResponseModel Get<TResponseModel, TErrorResult>(string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token) where TErrorResult : ErrorResult;

        public abstract object Get<TErrorResult>(Type responseModelType, string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token) where TErrorResult : ErrorResult;

        #endregion

        #endregion

#if !NET40
        #region Async

        #region Post

        public async Task<TResponseModel> PostAsync<TResponseModel>(string path)
        {
            return (TResponseModel)await PostAsync(typeof(TResponseModel), path);
        }

        public Task<object> PostAsync(Type responseModelType, string path)
        {
            return PostAsync<DefaultErrorResult>(responseModelType, path, null, null);
        }

        public async Task<TResponseModel> PostAsync<TResponseModel>(string path, object model)
        {
            return (TResponseModel)await PostAsync(typeof(TResponseModel), path, model);
        }

        public Task<object> PostAsync(Type responseModelType, string path, object model)
        {
            return PostAsync<DefaultErrorResult>(responseModelType, path, model, null);
        }

        public async Task<TResponseModel> PostAsync<TResponseModel>(string path, AuthorizationToken token)
        {
            return (TResponseModel)await PostAsync(typeof(TResponseModel), path, null, token);
        }

        public Task<object> PostAsync(Type responseModelType, string path, AuthorizationToken token)
        {
            return PostAsync<DefaultErrorResult>(responseModelType, path, null, token);
        }

        public async Task<TResponseModel> PostAsync<TResponseModel>(string path, object model, AuthorizationToken token)
        {
            return (TResponseModel)await PostAsync(typeof(TResponseModel), path, model, token);
        }

        public Task<object> PostAsync(Type responseModelType, string path, object model, AuthorizationToken token)
        {
            return PostAsync<DefaultErrorResult>(responseModelType, path, model, token);
        }

        public async Task<TResponseModel> PostAsync<TResponseModel, TErrorResult>(string path) where TErrorResult : ErrorResult
        {
            return (TResponseModel)await PostAsync<TErrorResult>(typeof(TResponseModel), path);
        }

        public Task<object> PostAsync<TErrorResult>(Type responseModelType, string path) where TErrorResult : ErrorResult
        {
            return PostAsync<TErrorResult>(responseModelType, path, null, null);
        }

        public async Task<TResponseModel> PostAsync<TResponseModel, TErrorResult>(string path, object model) where TErrorResult : ErrorResult
        {
            return (TResponseModel)await PostAsync<TErrorResult>(typeof(TResponseModel), path, model);
        }

        public Task<object> PostAsync<TErrorResult>(Type responseModelType, string path, object model) where TErrorResult : ErrorResult
        {
            return PostAsync<TErrorResult>(responseModelType, path, model, null);
        }

        public abstract Task<TResponseModel> PostAsync<TResponseModel, TErrorResult>(string path, object model, AuthorizationToken token) where TErrorResult : ErrorResult;

        public abstract Task<object> PostAsync<TErrorResult>(Type responseModelType, string path, object model, AuthorizationToken token) where TErrorResult : ErrorResult;

        #endregion

        #region Get

        public async Task<TResponseModel> GetAsync<TResponseModel>(string path)
        {
            return (TResponseModel)await GetAsync(typeof(TResponseModel), path);
        }

        public Task<object> GetAsync(Type responseModelType, string path)
        {
            return GetAsync<DefaultErrorResult>(responseModelType, path, null, null);
        }

        public async Task<TResponseModel> GetAsync<TResponseModel>(string path, IEnumerable<KeyValuePair<String, String>> parameters)
        {
            return (TResponseModel)await GetAsync(typeof(TResponseModel), path, parameters);
        }

        public Task<object> GetAsync(Type responseModelType, string path, IEnumerable<KeyValuePair<String, String>> parameters)
        {
            return GetAsync<DefaultErrorResult>(responseModelType, path, parameters, null);
        }

        public async Task<TResponseModel> GetAsync<TResponseModel>(string path, AuthorizationToken token)
        {
            return (TResponseModel) await GetAsync(typeof(TResponseModel), path, null, token);
        }

        public Task<object> GetAsync(Type responseModelType, string path, AuthorizationToken token)
        {
            return GetAsync<DefaultErrorResult>(responseModelType,path, null, token);
        }

        public async Task<TResponseModel> GetAsync<TResponseModel>(string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token)
        {
            return (TResponseModel) await GetAsync(typeof(TResponseModel), path, parameters, token);
        }

        public Task<object> GetAsync(Type responseModelType, string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token)
        {
            return GetAsync<DefaultErrorResult>(responseModelType,path, parameters, token);
        }

        public async Task<TResponseModel> GetAsync<TResponseModel, TErrorResult>(string path) where TErrorResult : ErrorResult
        {
            return (TResponseModel) await GetAsync<TErrorResult>(typeof(TResponseModel), path);
        }

        public Task<object> GetAsync<TErrorResult>(Type responseModelType, string path) where TErrorResult : ErrorResult
        {
            return GetAsync<TErrorResult>(responseModelType,path, null,null);
        }

        public async Task<TResponseModel> GetAsync<TResponseModel, TErrorResult>(string path, IEnumerable<KeyValuePair<String, String>> parameters) where TErrorResult : ErrorResult
        {
            return (TResponseModel) await GetAsync<TErrorResult>(typeof(TResponseModel), path, parameters);
        }

        public Task<object> GetAsync<TErrorResult>(Type responseModelType, string path, IEnumerable<KeyValuePair<String, String>> parameters) where TErrorResult : ErrorResult
        {
            return GetAsync<TErrorResult>(responseModelType,path, parameters, null);
        }

        public abstract Task<TResponseModel> GetAsync<TResponseModel, TErrorResult>(string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token) where TErrorResult : ErrorResult;

        public abstract Task<object> GetAsync<TErrorResult>(Type responseModelType, string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token) where TErrorResult : ErrorResult;

        #endregion

        #endregion
#endif

        #region Helpers

        protected string FixJson(string json)
        {
            //return json.Replace("\"[", "[").Replace("]\"", "]").Replace("\"{", "{").Replace("}\"", "}").Replace("\\", "").Replace("\\\"", "\"").Replace("\"[", "[").Replace("]\"", "]").Replace("\"{", "{").Replace("}\"", "}");
            return json;
        }

        protected string CreateQueryString(IEnumerable<KeyValuePair<String, String>> parameters)
        {
            try
            {
                if (parameters != null && parameters.Count() != 0)
                {
                    return String.Join("&", parameters.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected string AppendQueryString(string path, IEnumerable<KeyValuePair<String, String>> parameters)
        {
            string queryString = CreateQueryString(parameters);

            if (!String.IsNullOrEmpty(queryString))
            {
                if (path.Contains('?'))
                {
                    return String.Format("{0}&{1}", path, queryString);
                }
                else
                {
                    return String.Format("{0}?{1}", path, queryString);
                }
            }
            else
            {
                return path;
            }
        }

        protected Exception HandleException<TErrorResult>(HttpStatusCode statusCode, string content) where TErrorResult : ErrorResult
        {
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                return new UnauthorizedHttpException();
            }
            else if (statusCode == HttpStatusCode.BadRequest || statusCode == HttpStatusCode.InternalServerError)
            {
                return CreateAPIException<TErrorResult>(content);
            }
            else
            {
                return new HttpException("Unable to perform Http action.", content);
            }
        }

        private static Exception CreateAPIException<TErrorResult>(string content) where TErrorResult : ErrorResult
        {
            if (typeof(TErrorResult) == typeof(DefaultErrorResult))
            {
                JObject obj = null;

                try
                {
                    obj = JObject.Parse(content);
                }
                catch
                {
                    return new HttpException("Unable to perform Http action.", content);
                }

#if NET40
                if (obj["ModelState"] != null)
                {
                    return new HttpException<ModelStateErrorResult>("Unable to perform Http action.", content);
                }
                else if (obj["Message"] != null)
                {
                    return new HttpException<BadRequestErrorResult>("Unable to perform Http action.", content);
                }
                else
                {
                    return new HttpException("Unable to perform Http action.", content);
                }
#else
                if (obj.ContainsKey("ModelState"))
                {
                    return new HttpException<ModelStateErrorResult>("Unable to perform Http action.", obj);
                }
                else if (obj.ContainsKey("Message"))
                {
                    return new HttpException<BadRequestErrorResult>("Unable to perform Http action.", obj);
                }
                else
                {
                    return new HttpException("Unable to perform Http action.", content);
                }
#endif
            }
            else
            {
                return new HttpException<TErrorResult>("Unable to perform Http action.", content);
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {

        }

        #endregion
    }
}
