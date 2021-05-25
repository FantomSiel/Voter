using System;

namespace Voter.Core.Exceptions
{
    public static class Utility
    {
        public static T Handle<T>(Func<T> func, out ApiException exception)
        {
            exception = null;

            try
            {
                return func();
            }
            catch (ApiException e)
            {
                exception = e;
                return default(T);
            }
        }
    }
}
