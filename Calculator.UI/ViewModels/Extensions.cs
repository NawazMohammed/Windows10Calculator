using System;

namespace Calculator.Common
{
    public static class Extensions
    {
        public static T ToEnum<T>(this object o)
        {
            T enumVal = (T)Enum.Parse(typeof(T), o.ToString());
            return enumVal;
        }

    }
}
