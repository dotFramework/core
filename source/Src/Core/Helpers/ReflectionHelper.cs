using System.Diagnostics;
using System.Reflection;

namespace DotFramework.Core
{
    public static class ReflectionHelper
    {
        public static MethodBase GetCallerMethod()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();

            StackFrame callingFrame = stackFrames[2];
            MethodBase methodBase = callingFrame.GetMethod();

            return methodBase;
        }

        public static MethodBase GetCallerMethod(string methodName)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();

            MethodBase methodBase = null;

            foreach (StackFrame frame in stackFrames)
            {
                methodBase = frame.GetMethod();

                if (methodBase.Name == methodName)
                {
                    break;
                }
            }

            return methodBase;
        }
    }
}
