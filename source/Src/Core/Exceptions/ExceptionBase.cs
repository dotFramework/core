using System;
using System.Reflection;

namespace DotFramework.Core
{
    public class ExceptionBase : Exception
    {
        #region Constructors

        public ExceptionBase()
            : base()
        {

        }

        public ExceptionBase(string message)
            : base(message)
        {

        }

        public ExceptionBase(string message, string applicationCode, string className, string methodName)
            : base(message)
        {
            ClassName = className;
            MethodName = methodName;
        }

        public ExceptionBase(string message, string applicationCode, MethodBase methodBase) :
            this(message, applicationCode, methodBase.DeclaringType.FullName, methodBase.Name)
        {

        }

        public ExceptionBase(string message, Exception inner)
            : base(message, inner)
        {
            if (inner is ExceptionBase)
            {
                ApplicationCode = (inner as ExceptionBase).ApplicationCode;
                ClassName = (inner as ExceptionBase).ClassName;
                MethodName = (inner as ExceptionBase).MethodName;
            }
        }

        public ExceptionBase(string message, Exception inner, string applicationCode, string className, string methodName)
            : base(message, inner)
        {
            ApplicationCode = applicationCode;
            ClassName = className;
            MethodName = methodName;
        }

        public ExceptionBase(string message, Exception inner, string applicationCode, MethodBase methodBase) :
            this(message, inner, applicationCode, methodBase.DeclaringType.FullName, methodBase.Name)
        {

        }

        #endregion

        #region Properties

        private string _ApplicationCode;
        public string ApplicationCode
        {
            get
            {
                return _ApplicationCode;
            }
            set
            {
                _ApplicationCode = value;
                ModifyData("Application Code", value);
            }
        }

        private string _ClassName;
        public string ClassName
        {
            get
            {
                return _ClassName;
            }
            set
            {
                _ClassName = value;
                ModifyData("Class Name", value);
            }
        }

        private string _MethodName;
        public string MethodName
        {
            get
            {
                return _MethodName;
            }
            set
            {
                _MethodName = value;
                ModifyData("Method Name", value);
            }
        }

        #endregion

        #region Private Methods

        protected void ModifyData(string key, string value)
        {
            if (!Data.Contains(key))
            {
                Data.Add(key, value);
            }
            else
            {
                Data[key] = value;
            }
        }

        #endregion
    }
}
