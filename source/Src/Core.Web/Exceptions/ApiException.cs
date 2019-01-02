using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace DotFramework.Core
{
    public class HttpException : ExceptionBase
    {
        #region Properties

        public string ErrorContent { get; private set; }

        #endregion

        #region Constructors

        public HttpException()
        {
        }

        public HttpException(string message) : base(message)
        {
        }

        public HttpException(string message, string errorContent) : base(message)
        {
            ErrorContent = errorContent;
        }

        public HttpException(string message, Exception inner) : base(message, inner)
        {
        }

        public HttpException(string message, string errorContent, Exception inner) : base(message, inner)
        {
            ErrorContent = errorContent;
        }

        public HttpException(string message, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
        }

        public HttpException(string message, string errorContent, string applicationCode, MethodBase methodBase) : base(message, applicationCode, methodBase)
        {
            ErrorContent = errorContent;
        }

        public HttpException(string message, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
        }

        public HttpException(string message, string errorContent, string applicationCode, string className, string methodName) : base(message, applicationCode, className, methodName)
        {
            ErrorContent = errorContent;
        }

        public HttpException(string message, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
        }

        public HttpException(string message, string errorContent, Exception inner, string applicationCode, MethodBase methodBase) : base(message, inner, applicationCode, methodBase)
        {
            ErrorContent = errorContent;
        }

        public HttpException(string message, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
        }

        public HttpException(string message, string errorContent, Exception inner, string applicationCode, string className, string methodName) : base(message, inner, applicationCode, className, methodName)
        {
            ErrorContent = errorContent;
        }

        #endregion
    }

    public class HttpException<T> : HttpException
    {
        #region Constructor

        public HttpException(string message, string errorContent)
            : base(message, errorContent)
        {
            ErrorResult = JsonConvert.DeserializeObject<T>(errorContent);
        }

#if !NET40
        public HttpException(string message, JObject obj)
            : base(message)
        {
            ErrorResult = obj.ToObject<T>();
        }
#endif

        #endregion

        #region Properties

        public T ErrorResult { get; private set; }

        #endregion
    }

    public class UnauthorizedHttpException : HttpException
    {
        public UnauthorizedHttpException()
        {
        }

        public UnauthorizedHttpException(string message) : base(message)
        {
        }
    }
}
