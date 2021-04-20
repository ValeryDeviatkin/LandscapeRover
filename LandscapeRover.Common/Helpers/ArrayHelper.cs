using System;
using System.Collections.Generic;

namespace LandscapeRover.Common.Helpers
{
    public static class ArrayHelper
    {
        public static IEnumerable<T> ToEnumerable<T>(this Array target)
        {
            foreach (var item in target)
            {
                yield return (T) item;
            }
        }
    }
}