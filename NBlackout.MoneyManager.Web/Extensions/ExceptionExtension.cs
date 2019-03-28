using System;

namespace NBlackout.MoneyManager.Web.Extensions
{
    public static class ExceptionExtension
    {
        public static T FindInnerException<T>(this Exception exception) where T : Exception
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            var expectedException = exception as T;
            if (expectedException != null)
                return expectedException;

            var innerException = exception.InnerException;
            if (innerException == null)
                return null;

            return exception.InnerException.FindInnerException<T>();
        }
    }
}