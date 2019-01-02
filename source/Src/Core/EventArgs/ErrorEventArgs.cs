using System;

namespace DotFramework.Core
{
    public class ErrorEventArgs : EventArgs
    {
        public Exception Error { get; set; }

        public ErrorEventArgs(Exception ex) : base()
        {
            Error = ex;
        }
    }
}
