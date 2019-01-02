using DotFramework.Core.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Core.Web
{
    public partial class WebRequestUtility : AbstractHttpUtility
    {
        #region Constructor

        public WebRequestUtility(string baseAddress) : base(baseAddress)
        {
        }

        public WebRequestUtility(string baseAddress, IDictionary<String, String> defaultHeaders) : base(baseAddress, defaultHeaders)
        {

        }

        #endregion

        #region Sync

        #region Post
        /// <summary>
        /// This is a post call that wrap and simplify the exceptions
        /// </summary>
        /// <typeparam name="TResponseModel"></typeparam>
        /// <typeparam name="TErrorResult"></typeparam>
        /// <param name="path"></param>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public override TResponseModel Post<TResponseModel, TErrorResult>(string path, object model, AuthorizationToken token)
        {
            return (TResponseModel)Post<TErrorResult>(typeof(TResponseModel), path, model, token);
        }

        public override object Post<TErrorResult>(Type responseModelType, string path, object model, AuthorizationToken token)
        {
            string contentType;
            string content;

            if (model is IEnumerable<KeyValuePair<String, String>>)
            {
                contentType = "application/x-www-form-urlencoded";
                content = GetUrlEncodedRequestContent(model as IEnumerable<KeyValuePair<String, String>>);
            }
            else
            {
                contentType = ContentType;
                content = GetJsonRequestContent(model);
            }

            return Send<TErrorResult>(responseModelType,path, HttpMethod.Post, contentType, content, token);
        }

        

        #endregion

        #region Get

        public override TResponseModel Get<TResponseModel, TErrorResult>(string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token)
        {
            return (TResponseModel)Get<TErrorResult>(typeof(TResponseModel), path, parameters, token);
        }

        public override object Get<TErrorResult>(Type responseModelType, string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token)
        {
            try
            {
                return Send<TErrorResult>(responseModelType, AppendQueryString(path, parameters), HttpMethod.Get, null, null, token);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #endregion

#if !NET40
        #region Async

        public override async Task<TResponseModel> PostAsync<TResponseModel, TErrorResult>(string path, object model, AuthorizationToken token)
        {
            return (TResponseModel) await PostAsync<TErrorResult>(typeof(TResponseModel), path, model, token);
        }

        public override async Task<object> PostAsync<TErrorResult>(Type responseModelType, string path, object model, AuthorizationToken token)
        {
            return await Task.Run(() =>
            {
                return Post<TErrorResult>(responseModelType, path, model, token);
            });
        }

        public override async Task<TResponseModel> GetAsync<TResponseModel, TErrorResult>(string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token)
        {
            return (TResponseModel)await GetAsync<TErrorResult>(typeof(TResponseModel), path, parameters, token);
        }

        public override async Task<object> GetAsync<TErrorResult>(Type responseModelType, string path, IEnumerable<KeyValuePair<String, String>> parameters, AuthorizationToken token)
        {
            return await Task.Run(() =>
            {
                return Get<TErrorResult>(responseModelType,path, parameters, token);
            });
        }

        #endregion
#endif

        #region Helpers

        private TResponseModel Send<TResponseModel, TErrorResult>(string path, HttpMethod method, string contentType, string content, AuthorizationToken token) where TErrorResult : ErrorResult
        {
            return (TResponseModel) Send<TErrorResult>(typeof(TResponseModel), path, method, contentType, content, token);
        }

        private object Send<TErrorResult>(Type responseModelType, string path, HttpMethod method, string contentType, string content, AuthorizationToken token) where TErrorResult : ErrorResult
        {
            try
            {
                return PureSend<TErrorResult>(responseModelType, path, method, contentType, content, token);
            }
            catch (WebException ex)
            {
                if ((ex.Response as HttpWebResponse) != null)
                {
                    throw HandleException<TErrorResult>((ex.Response as HttpWebResponse).StatusCode, GetResponseContent(ex.Response));
                }
                else
                {
                    throw HandleException<TErrorResult>(HttpStatusCode.NotFound, ex.Message);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private object PureSend<TErrorResult>(Type responseModelType, string path, HttpMethod method, string contentType, string content, AuthorizationToken token) where TErrorResult : ErrorResult
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(BaseAddress, path));
                WriteHeaders(webRequest, method, contentType, token);
                WriteContent(webRequest, content);

                using (var response = webRequest.GetResponse())
                {
                    string responseStr = GetResponseContent(response);

                    if (responseModelType == typeof(String))
                    {
                        return responseStr;
                    }
                    else
                    {
                        return JsonSerializerHelper.Deserialize(responseModelType, responseStr);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void WriteHeaders(HttpWebRequest webRequest, HttpMethod method, string contentType, AuthorizationToken token)
        {
            try
            {
                webRequest.Accept = Accept;

                if (!String.IsNullOrEmpty(contentType))
                {
                    webRequest.ContentType = contentType;
                }

                webRequest.Method = method.Method;

                if (Timeout != 0)
                {
                    webRequest.Timeout = Timeout;
                }

                if (token != null)
                {
                    webRequest.PreAuthenticate = true;
                    webRequest.Headers.Add("Authorization", String.Format("{0} {1}", token.TokenType, token.Token));
                }

                if (DefaultHeaders != null)
                {
                    foreach (var headerItem in DefaultHeaders)
                    {
                        webRequest.Headers.Add(headerItem.Key, headerItem.Value);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void WriteContent(HttpWebRequest webRequest, string content)
        {
            try
            {
                if (!String.IsNullOrEmpty(content))
                {
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    Byte[] bytes = encoding.GetBytes(content);

                    Stream newStream = webRequest.GetRequestStream();
                    newStream.Write(bytes, 0, bytes.Length);
                    newStream.Close();
                }
                else
                {
                    webRequest.ContentLength = 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetUrlEncodedRequestContent(IEnumerable<KeyValuePair<String, String>> parameters)
        {
            return CreateQueryString(parameters);
        }

        private string GetJsonRequestContent(object model)
        {
            try
            {
                if (model != null)
                {
                    return JsonSerializerHelper.SimpleSerialize(model);
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

        private string GetResponseContent(WebResponse response)
        {
            try
            {
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    var content = reader.ReadToEnd();
                    return FixJson(content);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
