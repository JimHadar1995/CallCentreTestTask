using System;

namespace CallCentreConsoleProject.Exceptions
{
    internal class CallCentreException : Exception
    {
        public CallCentreException(string message, Exception? innerException = null)
            : base(message, innerException)
        {
        }
    }
}
