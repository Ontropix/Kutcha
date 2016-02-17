using System;

namespace Kutcha.Core.Extensions
{
    public static class KutchaRootExtensions
    {
        public static TReturn MaybeReturn<TReturn>(this IKutchaRoot root, Func<TReturn> returnFunc)
        {
            return root != null ? returnFunc() : default(TReturn);
        }

        public static TReturn MaybeReturn<TReturn>(this IKutchaRoot root, TReturn returnObj)
        {
            return root != null ? returnObj : default(TReturn);
        }
    }
}
