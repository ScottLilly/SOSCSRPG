using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public static class ExtensionMethods
    {
        public static bool None<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            return !list.Any(predicate);
        }
    }
}